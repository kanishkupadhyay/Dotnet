using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.DTOs.Request;
using MyWebApi.Services.CQRS.Command.Login;
using MyWebApi.Services.CQRS.Command.Register;
using MyWebApi.Services.CQRS.Query.GetUsers;
using MyWebApi.Services.DTOs.Request;

namespace MyWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _mediator.Send(new RegisterCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword
        });

        return StatusCode(response.Status, response);
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

    [HttpGet("users")]
    [Authorize]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQueryRequest query)
    {
        var response = await _mediator.Send(new GetUsersQuery(query));

        return StatusCode(response.Status, response);
    }
}
