using MediatR;
using MyWebApi.Services.DTOs.Response;

namespace MyWebApi.Services.CQRS.Query.GetBlogById;

public class GetBlogByIdQuery : IRequest<ApiResponse<BlogResponse>>
{
    public Guid Id { get; set; }
}