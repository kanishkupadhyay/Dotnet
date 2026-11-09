using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyWebApi.Swagger;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(
        OpenApiOperation operation,
        OperationFilterContext context)
    {
        var method = context.MethodInfo;
        var controller = method.DeclaringType;

        var hasAuthorize =
            method.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
            ||
            controller?.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Any() == true;

        var hasAllowAnonymous =
            method.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any()
            ||
            controller?.GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>().Any() == true;

        if (!hasAuthorize || hasAllowAnonymous)
            return;

        operation.Security ??= [];

        operation.Security.Add(
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(
                    "Bearer",
                    context.Document
                )] = []
            }
        );
    }
}