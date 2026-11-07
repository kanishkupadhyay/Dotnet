
using MediatR;
using MyWebApi.Domain.Interfaces;
using MyWebApi.Services.Constants;
using MyWebApi.Services.DTOs.Response;
using MyWebApi.Services.Interfaces;
using MyWebApi.Services.Validators;

namespace MyWebApi.Services.CQRS.Command.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginResponse>>
{

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<ApiResponse<LoginResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
        {
            return ApiResponse<LoginResponse>.BadRequest(ErrorMessages.EmailCannotBeNullOrEmpty);
        }

        bool isEmailValid = EmailValidator.IsValid(command.Email);
        if (!isEmailValid)
        {
            return ApiResponse<LoginResponse>.BadRequest(ErrorMessages.InvalidEmail);
        }

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            return ApiResponse<LoginResponse>.BadRequest(ErrorMessages.PasswordCannotBeNullOrEmpty);
        }

        var user = await _userRepository.GetByEmailAsync(command.Email);

        if (user == null)
        {
            return ApiResponse<LoginResponse>.BadRequest(ErrorMessages.InvalidCredentials);
        }

        bool isPasswordValid = _passwordHasher.Verify(command.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return ApiResponse<LoginResponse>.BadRequest(ErrorMessages.InvalidCredentials);
        }

        var token = _tokenService.GenerateToken(user);
        var response = new LoginResponse
        {
            AccessToken = token,
        };

        return ApiResponse<LoginResponse>.OkResponse(SuccessMessages.LoginSuccessful, response);
    }
}