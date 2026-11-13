using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebApi.Services.DTOs.Request;
using MyWebApi.Services.DTOs.Response;

namespace MyWebApi.Services.CQRS.Query.GetBlogListQuery;

public class GetBlogListQuery : IRequest<ApiResponse<List<BlogResponse>>>
{
    public int Page { get; set; }
    public int Size { get; set; }

    public GetBlogListQuery(GePaginationRequest query)
    {
        Page = query.Page;
        Size = query.Size;
    }
}