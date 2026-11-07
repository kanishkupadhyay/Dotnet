using Microsoft.AspNetCore.Mvc;
using MyWebApi.Services;
using MyWebApi.Services.Extensions;
using MyWebApi.Infrastructure.Extensions;
using MyWebApi.Domain.Interfaces;
using MyWebApi.Infrastructure.Repositories;
using MyWebApi.Services.Security;
using MyWebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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


builder.Services.AddApplicationCqrs();
builder.Services.AddInfrastructure(builder.Configuration);

// Add UserRepository to DI container

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();