using System.Diagnostics;

namespace MyWebApi.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        var method = context.Request.Method;
        var path = context.Request.Path;

        _logger.LogInformation(
            "Request Started: {Method} {Path}",
            method, path
        );

        try
        {
            await _next(context);

            stopwatch.Stop();

            var statusCode = context.Response.StatusCode;

            if (statusCode >= 500)
            {
                _logger.LogError(
                    "Server Error: {Method} {Path} | Status: {StatusCode} | Duration: {Duration}ms",
                    method, path, statusCode, stopwatch.ElapsedMilliseconds
                );
            }
            else if (statusCode >= 400)
            {
                _logger.LogWarning(
                    "Client Error: {Method} {Path} | Status: {StatusCode} | Duration: {Duration}ms",
                    method, path, statusCode, stopwatch.ElapsedMilliseconds
                );
            }
            else
            {
                _logger.LogInformation(
                    "Request Completed: {Method} {Path} | Status: {StatusCode} | Duration: {Duration}ms",
                    method, path, statusCode, stopwatch.ElapsedMilliseconds
                );
            }
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(
                ex,
                "Unhandled Exception: {Method} {Path}",
                method, path
            );

            throw;
        }
    }


}