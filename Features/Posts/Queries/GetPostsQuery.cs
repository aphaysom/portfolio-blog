using Mediator;
using PortainerBlog.DTOs;
using PortainerBlog.Repositories;

namespace PortainerBlog.Features.Posts.Queries;

public record GetPostsQuery() : IRequest<IEnumerable<PostResponse>>;

public class GetPostsHandler(IPostRepository repository)
    : IRequestHandler<GetPostsQuery, IEnumerable<PostResponse>>
{
    public async ValueTask<IEnumerable<PostResponse>> Handle(
        GetPostsQuery request,
        CancellationToken cancellationToken
    )
    {
        var posts = await repository.GetAllAsync();
        return posts.Select(p => new PostResponse(p.Id, p.Title, p.Content, p.CreatedAt));
    }
}
