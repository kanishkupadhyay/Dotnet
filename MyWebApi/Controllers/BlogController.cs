using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Services.Constants;
using MyWebApi.Services.CQRS.Command.CreateBlog;
using MyWebApi.Services.CQRS.Command.DeleteBlogById;
using MyWebApi.Services.CQRS.Query.GetBlogById;
using MyWebApi.Services.DTOs.Request;

namespace MyWebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BlogController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlogController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<IActionResult> createBlog([FromBody] CreateBlogCommandRequest command)
    {
        var response = await _mediator.Send(new CreateBlogCommand
        {
            Title = command.Title,
            Content = command.Content,
            ImageUrl = command.ImageUrl
        });

        return StatusCode(response.Status, response);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> getBlogById([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetBlogByIdQuery { Id = id });

        return StatusCode(response.Status, response);
    }


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> deleteBlog([FromRoute] Guid id)
    {
        var response = _mediator.Send(new DeleteBlogByIdCommand { Id = id });
        return StatusCode((int)response.Status, response);
    }

    [HttpPatch("update/{id}")]
    public Task<IActionResult> updateBlog([FromRoute] Guid id)
    {
        return Task.FromResult<IActionResult>(Ok(SuccessMessages.BlogFetchedSuccessfully));
    }
}