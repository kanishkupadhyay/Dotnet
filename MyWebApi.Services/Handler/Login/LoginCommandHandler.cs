
using MediatR;
using MyWebApi.Services.Constants;

namespace MyWebApi.Services.CQRS.Command.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<object>>
{
    public Task<ApiResponse<object>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
        {
            return Task.FromResult(ApiResponse<object>.BadRequest(ErrorMessages.EmailCannotBeNullOrEmpty));
        }

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            return Task.FromResult(ApiResponse<object>.BadRequest(ErrorMessages.PasswordCannotBeNullOrEmpty));
        }

        return Task.FromResult(ApiResponse<object>.OkResponse(SuccessMessages.LoginSuccessful));
    }
}