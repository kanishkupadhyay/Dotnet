using MyWebApi.Constants;
using MyWebApi.DTOs.Request;
using MyWebApi.DTOs.Response;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;


namespace MyWebApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task Register(RegisterRequest request)
    {
        // Registration logic will go here
        return Task.CompletedTask;
    }

    public Task<ApiResponse<object>> Login(LoginRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.email))
            {
                return Task.FromResult(ApiResponse<object>.BadRequest(ErrorMessages.EmailCannotBeNullOrEmpty));
            }

            if (string.IsNullOrWhiteSpace(request.password))
            {
                return Task.FromResult(ApiResponse<object>.BadRequest(ErrorMessages.PasswordCannotBeNullOrEmpty));
            }

            return Task.FromResult(ApiResponse<object>.OkResponse(SuccessMessages.LoginSuccessful));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during login: {ex.Message}");
            return Task.FromResult(ApiResponse<object>.BadRequest(ex.Message));
        }
    }
}