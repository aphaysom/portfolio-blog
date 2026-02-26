using Mediator;
using Microsoft.Extensions.Caching.Hybrid;
using PortainerBlog.DTOs;
using PortainerBlog.Models;
using PortainerBlog.Repositories;

namespace PortainerBlog.Features.Posts.Commands;

public record CreatePostCommand(string Title, string Content) : IRequest<PostResponse>;

#pragma warning disable EXTEXP0018
public class CreatePostHandler(IPostRepository repository, HybridCache cache)
    : IRequestHandler<CreatePostCommand, PostResponse>
{
    public async ValueTask<PostResponse> Handle(
        CreatePostCommand request,
        CancellationToken cancellationToken
    )
    {
        Post post = new() { Title = request.Title, Content = request.Content };

        await repository.AddAsync(post);
        await repository.SaveChangesAsync();

        // Invalidate cached posts so the next read fetches fresh data
        await cache.RemoveByTagAsync("posts", cancellationToken);

        return new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt);
    }
}
#pragma warning restore EXTEXP0018
