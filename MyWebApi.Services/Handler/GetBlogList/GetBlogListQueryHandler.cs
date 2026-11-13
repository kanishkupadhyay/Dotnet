using MediatR;
using MyWebApi.Domain.Entities;
using MyWebApi.Services.Constants;
using MyWebApi.Services.DTOs.Response;
using MyWebApi.Services.Mappers;

namespace MyWebApi.Services.CQRS.Query.GetBlogListQuery;

public class GetBlogListQueryHandler : IRequestHandler<GetBlogListQuery, ApiResponse<List<BlogResponse>>>
{
    private readonly IBlogRepository blogRepository;

    public GetBlogListQueryHandler(IBlogRepository _blogRepository)
    {
        blogRepository = _blogRepository;
    }
    public async Task<ApiResponse<List<BlogResponse>>> Handle(GetBlogListQuery query, CancellationToken cancellationToken)
    {
        var blogs = await blogRepository.GetBlogListWithPagination(query.Page, query.Size);
        var blogResponse = blogs.Select(blog => blog.ToResponse()).ToList();

        return ApiResponse<List<BlogResponse>>.OkResponse(SuccessMessages.BlogsFetchedSuccessfully, blogResponse);
    }
}