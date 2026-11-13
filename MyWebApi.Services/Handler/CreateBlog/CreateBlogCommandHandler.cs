using MediatR;
using MyWebApi.Domain.Entities;
using MyWebApi.Services.Constants;
using MyWebApi.Services.DTOs.Response;
using MyWebApi.Services.Interfaces;
using MyWebApi.Services.Mappers;

namespace MyWebApi.Services.CQRS.Command.CreateBlog;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, ApiResponse<BlogResponse>>
{
    private readonly IBlogRepository _blogRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateBlogCommandHandler(IBlogRepository blogRepository, ICurrentUserService currentUserService)
    {
        _blogRepository = blogRepository;
        _currentUserService = currentUserService;
    }
    public async Task<ApiResponse<BlogResponse>> Handle(CreateBlogCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(command.Title) || string.IsNullOrEmpty(command.Title))
            {
                return ApiResponse<BlogResponse>.BadRequest(ErrorMessages.TitleCannotBeNullOrEmpty);
            }

            if (string.IsNullOrWhiteSpace(command.Content) || string.IsNullOrEmpty(command.Content))
            {
                return ApiResponse<BlogResponse>.BadRequest(ErrorMessages.ContentCannotBeNullOrEmpty);
            }
            var blog = new Blog
            {
                Title = command.Title,
                Content = command.Content,
                ImageUrl = command.ImageUrl,
                AuthorId = _currentUserService.UserId ?? throw new InvalidOperationException(ErrorMessages.UserIdNotAvailable),
            };

            await _blogRepository.CreateBlogAsync(blog, cancellationToken);

            return ApiResponse<BlogResponse>.CreatedResponse(SuccessMessages.BlogCreatedSuccessfully, blog.ToResponse());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while creating blog: {ex.Message}");
            return ApiResponse<BlogResponse>.InternalServerError(ErrorMessages.InternalServerError);
        }
    }
}