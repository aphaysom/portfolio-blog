using PortainerBlog.DTOs;
using PortainerBlog.Models;
using PortainerBlog.Repositories;

namespace PortainerBlog.Services;

public class PostService(IPostRepository repository) : IPostService
{
    public async Task<IEnumerable<PostResponse>> GetPostsAsync()
    {
        IEnumerable<Post> posts = await repository.GetAllAsync();
        return posts.Select(p => new PostResponse(p.Id, p.Title, p.Content, p.CreatedAt));
    }

    public async Task<PostResponse?> GetPostAsync(Guid id)
    {
        Post? post = await repository.GetByIdAsync(id);

        if (post is null)
            return null;

        return new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt);
    }

    public async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
    {
        Post post = new() { Title = request.Title, Content = request.Content };

        await repository.AddAsync(post);
        await repository.SaveChangesAsync();

        return new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt);
    }
}
