using FastEndpoints;
using PortainerBlog.DTOs;
using PortainerBlog.Services;

namespace PortainerBlog.Features.Posts.Endpoints;

public class GetAllPostsEndpoint(IPostService postService)
    : EndpointWithoutRequest<IEnumerable<PostResponse>>
{
    public override void Configure()
    {
        Get("/api/posts");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<PostResponse> posts = await postService.GetPostsAsync();
        await SendAsync(posts, cancellation: ct);
    }
}
