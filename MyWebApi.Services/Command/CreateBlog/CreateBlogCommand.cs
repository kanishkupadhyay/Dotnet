using MediatR;
using MyWebApi.Services.DTOs.Response;

namespace MyWebApi.Services.CQRS.Command.CreateBlog;

public class CreateBlogCommand : IRequest<ApiResponse<BlogResponse>>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

}