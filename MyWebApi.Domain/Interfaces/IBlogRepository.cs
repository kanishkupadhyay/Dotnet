namespace MyWebApi.Domain.Entities;

public interface IBlogRepository
{
    Task CreateBlogAsync(Blog blog);
    Task<Blog?> GetBlogByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Blog?> DeleteBlogById(Guid id, CancellationToken cancellationToken);
}