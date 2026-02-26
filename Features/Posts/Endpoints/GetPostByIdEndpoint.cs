using FastEndpoints;
using Mediator;
using PortainerBlog.DTOs;
using PortainerBlog.Features.Posts.Queries;

namespace PortainerBlog.Features.Posts.Endpoints;

public class GetPostByIdEndpoint(IMediator mediator) : EndpointWithoutRequest<PostResponse>
{
    public override void Configure()
    {
        Get("/api/posts/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Guid id = Route<Guid>("id");
        IEnumerable<PostResponse> posts = await mediator.Send(new GetPostsQuery(), ct);
        PostResponse? post = posts.FirstOrDefault(p => p.Id == id);

        if (post is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(post, cancellation: ct);
    }
}
