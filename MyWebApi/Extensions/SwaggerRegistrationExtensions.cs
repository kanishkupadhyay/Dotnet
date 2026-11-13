using Microsoft.OpenApi;
using MyWebApi.Swagger;

namespace MyWebApi.Extensions;

public static class SwaggerRegistrationExtension
{
    public static IServiceCollection AddSwaggerExtensions(this IServiceCollection services)
    {

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter your JWT token"
            });

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        return services;
    }
}