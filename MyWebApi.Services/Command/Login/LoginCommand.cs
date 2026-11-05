using MediatR;

namespace MyWebApi.Services.CQRS.Command.Login;

public class LoginCommand : IRequest<ApiResponse<object>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}