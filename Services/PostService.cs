using Microsoft.EntityFrameworkCore;
using PortainerBlog.Data;
using PortainerBlog.DTOs;
using PortainerBlog.Models;

namespace PortainerBlog.Services;

public class PostService(BlogDbContext context) : IPostService
{
    public async Task<IEnumerable<PostResponse>> GetPostsAsync()
    {
        return await context
            .Posts.OrderByDescending(p => p.CreatedAt)
            .Select(p => new PostResponse(p.Id, p.Title, p.Content, p.CreatedAt))
            .ToListAsync();
    }

    public async Task<PostResponse?> GetPostAsync(Guid id)
    {
        var post = await context.Posts.FindAsync(id);

        if (post is null)
            return null;

        return new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt);
    }

    public async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
    {
        var post = new Post { Title = request.Title, Content = request.Content };

        context.Posts.Add(post);
        await context.SaveChangesAsync();

        return new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt);
    }
}
