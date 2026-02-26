using Microsoft.EntityFrameworkCore;
using PortainerBlog.Data;
using PortainerBlog.Models;

namespace PortainerBlog.Repositories;

public class PostRepository(BlogDbContext context) : IPostRepository
{
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        return await context.Posts.FindAsync(id);
    }

    public async Task AddAsync(Post post)
    {
        await context.Posts.AddAsync(post);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
