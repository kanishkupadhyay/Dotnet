using MediatR;
using MyWebApi.Domain.Entities;
using MyWebApi.Domain.Interfaces;
using MyWebApi.Services.Constants;
using MyWebApi.Services.Interfaces;
using MyWebApi.Services.Validators;

namespace MyWebApi.Services.CQRS.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<object>>
{

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<ApiResponse<object>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.FirstName))
        {
            return ApiResponse<object>.BadRequest(ErrorMessages.FirstNameCannotBeNullOrEmpty);
        }

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            return ApiResponse<object>.BadRequest("Email cannot be null or empty.");
        }

        bool isEmailValid = EmailValidator.IsValid(command.Email);
        if (!isEmailValid)
        {
            return ApiResponse<object>.BadRequest(ErrorMessages.InvalidEmail);
        }

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            return ApiResponse<object>.BadRequest("Password cannot be null or empty.");
        }

        if (command.Password.Length < 8)
        {
            return ApiResponse<object>.BadRequest(ErrorMessages.PasswordTooShort);
        }

        if (command.Password.Length > 100)
        {
            return ApiResponse<object>.BadRequest(ErrorMessages.PasswordTooLong);
        }

        if (string.IsNullOrWhiteSpace(command.ConfirmPassword))
        {
            return ApiResponse<object>.BadRequest(ErrorMessages.ConfirmPasswordCannotBeNullOrEmpty);
        }

        if (command.Password != command.ConfirmPassword)
        {
            return ApiResponse<object>.BadRequest(ErrorMessages.PasswordsDoNotMatch);
        }

        var existingUser = await _userRepository.GetByEmailAsync(command.Email);

        if (existingUser != null)
        {
            return ApiResponse<object>.BadRequest(ErrorMessages.UserAlreadyExists);
        }

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            PasswordHash = _passwordHasher.Hash(command.Password)
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        return ApiResponse<object>.OkResponse(SuccessMessages.RegistrationSuccessful);
    }
}