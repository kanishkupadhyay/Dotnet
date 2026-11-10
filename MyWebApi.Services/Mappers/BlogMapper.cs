using MyWebApi.Domain.Entities;
using MyWebApi.Services.DTOs.Response;

namespace MyWebApi.Services.Mappers;

public static class BlogMapper
{
    public static BlogResponse ToResponse(this Blog blog)
    {
        return new BlogResponse
        {
            Id = blog.Id,
            Title = blog.Title,
            Content = blog.Content,
            ImageUrl = blog.ImageUrl,
            AuthorId = blog.AuthorId,
            CreatedAt = blog.CreatedAt
        };
    }
}
