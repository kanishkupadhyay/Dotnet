using Microsoft.EntityFrameworkCore;
using MyWebApi.Domain.Entities;
using MyWebApi.Infrastructure.Persistence;
using MyWebApi.Services.DTOs.Request;

namespace MyWebApi.Infrastructure.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly AppDbContext _dbContext;
    public BlogRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateBlogAsync(Blog blog, CancellationToken cancellationToken)
    {
        await _dbContext.Blogs.AddAsync(blog);
        await SaveChangesAsync(cancellationToken);
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

         await SaveChangesAsync(cancellationToken);

        return deletedBlog;
    }

    public async Task<List<Blog>> GetBlogListWithPagination(int page, int size)
    {
        return await _dbContext.Blogs.Skip((page - 1) * size).Take(size).ToListAsync();
    }

    public async Task<Blog?> UpdateBlogByIdAsync(
    Guid id,
    Blog updatedBlog,
    CancellationToken cancellationToken)
    {
        var existingBlog = await _dbContext.Blogs
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (existingBlog == null)
        {
            return null;
        }
        if (!string.IsNullOrEmpty(updatedBlog.Title))
        {
            existingBlog.Title = updatedBlog.Title;
        }
        if (!string.IsNullOrEmpty(updatedBlog.Content))
        {
            existingBlog.Content = updatedBlog.Content;
        }
        if (!string.IsNullOrEmpty(updatedBlog.ImageUrl))
        {
            existingBlog.ImageUrl = updatedBlog.ImageUrl;
        }

        await SaveChangesAsync(cancellationToken);

        return existingBlog;
    }
}