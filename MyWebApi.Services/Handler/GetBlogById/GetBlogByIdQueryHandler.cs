using MediatR;
using MyWebApi.Domain.Entities;
using MyWebApi.Services.Constants;
using MyWebApi.Services.DTOs.Response;
using MyWebApi.Services.Mappers;

namespace MyWebApi.Services.CQRS.Query.GetBlogById;

public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, ApiResponse<BlogResponse>>
{
    private readonly IBlogRepository _blogRepository;

    public GetBlogByIdQueryHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<ApiResponse<BlogResponse>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var blog = await _blogRepository.GetBlogByIdAsync(request.Id, cancellationToken);

            if (blog == null)
            {
                return ApiResponse<BlogResponse>.NotFound(ErrorMessages.BlogNotFound);
            }

            return ApiResponse<BlogResponse>.OkResponse(SuccessMessages.BlogFetchedSuccessfully, blog.ToResponse());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while retrieving blog: {ex.Message}");
            return ApiResponse<BlogResponse>.InternalServerError(ErrorMessages.InternalServerError);
        }
    }
}