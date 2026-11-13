
using MediatR;
using MyWebApi.Domain.Entities;
using MyWebApi.Services.Constants;
using MyWebApi.Services.DTOs.Response;
using MyWebApi.Services.Mappers;

namespace MyWebApi.Services.CQRS.Command.UpdateBlogById;

public class UpdateBlogByIdCommandHandler :
    IRequestHandler<UpdateBlogByIdCommand, ApiResponse<BlogResponse>>
{
    private readonly IBlogRepository _blogRepository;

    public UpdateBlogByIdCommandHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<ApiResponse<BlogResponse>> Handle(
        UpdateBlogByIdCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var blog = new Blog
            {
                Title = command.Command.Title,
                Content = command.Command.Content,
                ImageUrl = command.Command.ImageUrl
            };

            var updatedBlog = await _blogRepository.UpdateBlogByIdAsync(
                command.Id,
                blog,
                cancellationToken
            );

            if (updatedBlog == null)
            {
                return ApiResponse<BlogResponse>.BadRequest(
                    ErrorMessages.BlogNotFound
                );
            }

            return ApiResponse<BlogResponse>.CreatedResponse(
                SuccessMessages.BlogUpdatedSuccessfully,
                updatedBlog.ToResponse()
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while updating blog: {ex.Message}");

            return ApiResponse<BlogResponse>.InternalServerError(
                ErrorMessages.InternalServerError
            );
        }
    }
}
