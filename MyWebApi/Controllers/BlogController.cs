using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Services.Constants;

namespace MyWebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BlogController : ControllerBase
{
    [HttpPost("create")]
    public Task<IActionResult> createBlog()
    {
        return Task.FromResult<IActionResult>(Ok(SuccessMessages.BlogCreatedSuccessfully));
    }

    [HttpGet("get/{id}")]
    public Task<IActionResult> getBlog(Guid id)
    {
        return Task.FromResult<IActionResult>(Ok(SuccessMessages.BlogFetchedSuccessfully));
    }
}