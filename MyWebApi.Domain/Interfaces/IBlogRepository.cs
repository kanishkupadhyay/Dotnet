namespace MyWebApi.Domain.Entities;

public interface IBlogRepository
{
    Task CreateBlogAsync(Blog blog, CancellationToken cancellationToken);
    Task<Blog?> GetBlogByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Blog?> DeleteBlogById(Guid id, CancellationToken cancellationToken);
    Task<List<Blog>> GetBlogListWithPagination(int page, int size);
    Task<Blog?> UpdateBlogByIdAsync(Guid id, Blog updatedBlog, CancellationToken cancellationToken);
}