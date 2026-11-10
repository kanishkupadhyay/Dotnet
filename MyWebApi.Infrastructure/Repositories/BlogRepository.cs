using Microsoft.EntityFrameworkCore;
using MyWebApi.Domain.Entities;
using MyWebApi.Infrastructure.Persistence;

namespace MyWebApi.Infrastructure.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly AppDbContext _dbContext;
    public BlogRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public async Task CreateBlogAsync(Blog blog)
    {
        await _dbContext.Blogs.AddAsync(blog);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Blog?> GetBlogByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<Blog?> DeleteBlogById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deletedBlog = await _dbContext.Blogs
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (deletedBlog == null)
        {
            return null;
        }

        _dbContext.Blogs.Remove(deletedBlog);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return deletedBlog;
    }
}