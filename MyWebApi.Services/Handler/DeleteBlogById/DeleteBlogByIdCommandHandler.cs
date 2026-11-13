using MediatR;
using MyWebApi.Domain.Entities;
using MyWebApi.Services.Constants;
using MyWebApi.Services.DTOs.Response;
using MyWebApi.Services.Mappers;

namespace MyWebApi.Services.CQRS.Command.DeleteBlogById;

public class DeleteBlogByIdCommandHandler : IRequestHandler<DeleteBlogByIdCommand, ApiResponse<BlogResponse>>
{
    private readonly IBlogRepository _blogRepository;

    public DeleteBlogByIdCommandHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }
    public async Task<ApiResponse<BlogResponse>> Handle(DeleteBlogByIdCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var blog = await _blogRepository.DeleteBlogById(command.Id, cancellationToken);
            if (blog == null)
            {
                return ApiResponse<BlogResponse>.BadRequest(ErrorMessages.BlogNotFound);
            }
            return ApiResponse<BlogResponse>.OkResponse(SuccessMessages.BlogDeletedSuccessfully, blog.ToResponse());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while deleting blog with ID {command.Id}: {ex.Message}");
            return ApiResponse<BlogResponse>.InternalServerError(ErrorMessages.InternalServerError);
        }
    }
}