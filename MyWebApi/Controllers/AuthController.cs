using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.DTOs.Request;
using MyWebApi.DTOs.Response;
using MyWebApi.Services.CQRS.Command.Login;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public AuthController(IAuthService authService, IMediator mediator)
    {
        _authService = authService;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        await _authService.Register(request);
        return Ok(new ApiResponse<object>
        {
            Status = 200,
            Success = true,
            Message = "User registered successfully"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _mediator.Send(new LoginCommand
        {
            Email = request.email,
            Password = request.password
        });

        return StatusCode(response.Status, response);
    }
}