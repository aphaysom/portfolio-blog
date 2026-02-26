using Mediator;
using PortainerBlog.DTOs;
using PortainerBlog.Models;
using PortainerBlog.Repositories;

namespace PortainerBlog.Features.Posts.Commands;

public record CreatePostCommand(string Title, string Content) : IRequest<PostResponse>;

public class CreatePostHandler(IPostRepository repository)
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

        return new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt);
    }
}
