using FastEndpoints;
using PortainerBlog.DTOs;
using PortainerBlog.Services;

namespace PortainerBlog.Features.Posts.Endpoints;

public class GetPostByIdEndpoint(IPostService postService) : EndpointWithoutRequest<PostResponse>
{
    public override void Configure()
    {
        Get("/api/posts/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Guid id = Route<Guid>("id");
        PostResponse? post = await postService.GetPostAsync(id);

        if (post is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(post, cancellation: ct);
    }
}
