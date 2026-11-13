using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Services.Constants;
using MyWebApi.Services.CQRS.Command.CreateBlog;
using MyWebApi.Services.CQRS.Command.DeleteBlogById;
using MyWebApi.Services.CQRS.Command.UpdateBlogById;
using MyWebApi.Services.CQRS.Query.GetBlogById;
using MyWebApi.Services.CQRS.Query.GetBlogListQuery;
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
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogCommandRequest command)
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
    public async Task<IActionResult> GetBlogById([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetBlogByIdQuery { Id = id });

        return StatusCode(response.Status, response);
    }


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBlogById([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new DeleteBlogByIdCommand { Id = id });
        return StatusCode(response.Status, response);
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateBlog([FromRoute] Guid id, [FromBody] UpdateBlogRequest command)
    {
        var response = await _mediator.Send(new UpdateBlogByIdCommand(id, new UpdateBlogRequest
        {
            Title = command.Title,
            Content = command.Content,
            ImageUrl = command.ImageUrl
        }));
        return StatusCode(response.Status, response);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetBlogList([FromQuery] GePaginationRequest query)
    {
        var response = await _mediator.Send(new GetBlogListQuery(query));
        return StatusCode(response.Status, response);
    }
}