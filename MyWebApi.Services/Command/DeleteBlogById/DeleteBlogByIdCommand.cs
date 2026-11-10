using MediatR;
using MyWebApi.Services.DTOs.Response;

namespace MyWebApi.Services.CQRS.Command.DeleteBlogById;

public class DeleteBlogByIdCommand : IRequest<ApiResponse<BlogResponse>>
{
    public Guid Id { get; set; }
}