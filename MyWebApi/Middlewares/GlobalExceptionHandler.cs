using Microsoft.AspNetCore.Diagnostics;

namespace MyWebApi.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "Unhandled exception occurred"
        );

        context.Response.StatusCode = 500;

        await context.Response.WriteAsJsonAsync(new
        {
            Status = 500,
            Success = false,
            Message = "Something went wrong.",
            TraceId = context.TraceIdentifier
        }, cancellationToken);

        return true;
    }
}