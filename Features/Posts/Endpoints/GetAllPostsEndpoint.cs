using FastEndpoints;
using Mediator;
using PortainerBlog.DTOs;
using PortainerBlog.Features.Posts.Queries;

namespace PortainerBlog.Features.Posts.Endpoints;

public class GetAllPostsEndpoint(IMediator mediator)
    : EndpointWithoutRequest<IEnumerable<PostResponse>>
{
    public override void Configure()
    {
        Get("/api/posts");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<PostResponse> posts = await mediator.Send(new GetPostsQuery(), ct);
        await SendAsync(posts, cancellation: ct);
    }
}
