using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Registry;
using PortainerBlog.Data;
using PortainerBlog.Models;

namespace PortainerBlog.Repositories;

public class PostRepository(
    BlogDbContext context,
    ResiliencePipelineProvider<string> pipelineProvider
) : IPostRepository
{
    private readonly ResiliencePipeline _resiliencePipeline = pipelineProvider.GetPipeline(
        "db-pipeline"
    );

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _resiliencePipeline.ExecuteAsync(async _ =>
            await context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync()
        );
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        return await _resiliencePipeline.ExecuteAsync(async _ => await context.Posts.FindAsync(id));
    }

    public async Task AddAsync(Post post)
    {
        await _resiliencePipeline.ExecuteAsync(async _ => await context.Posts.AddAsync(post));
    }

    public async Task SaveChangesAsync()
    {
        await _resiliencePipeline.ExecuteAsync(async _ => await context.SaveChangesAsync());
    }
}
