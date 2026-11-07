using MediatR;
using MyWebApi.Services.DTOs.Response;

namespace MyWebApi.Services.CQRS.Command.Login;

public class LoginCommand : IRequest<ApiResponse<LoginResponse>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}