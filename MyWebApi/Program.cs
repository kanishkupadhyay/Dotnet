using Microsoft.AspNetCore.Mvc;
using MyWebApi.Services;
using MyWebApi.Services.Extensions;
using MyWebApi.Infrastructure.Extensions;
using MyWebApi.Domain.Interfaces;
using MyWebApi.Infrastructure.Repositories;
using MyWebApi.Services.Security;
using MyWebApi.Services.Interfaces;
using MyWebApi.Domain.Entities;
using MyWebApi.Middlewares;
using MyWebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// JWT Authentication
builder.Services.AddCustomJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddCustomRateLimiting();

// Customize DTO Validation Error Response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .ToDictionary(
                x => x.Key,
                x => x.Value!.Errors
                    .Select(e => e.ErrorMessage)
                    .ToArray()
            );

        var response = new ApiResponse<object>
        {
            Status = 400,
            Success = false,
            Message = "Validation failed",
            Data = errors
        };

        return new BadRequestObjectResult(response);
    };
});

// CQRS and Infrastructure
builder.Services.AddApplicationCqrs();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();

// Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtensions();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.EnablePersistAuthorization();
    });
}

app.UseExceptionHandler();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();