namespace MyWebApi.Services.DTOs.Response;

public class BlogResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
}