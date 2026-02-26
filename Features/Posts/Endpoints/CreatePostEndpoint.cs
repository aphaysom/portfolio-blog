using FastEndpoints;
using PortainerBlog.DTOs;
using PortainerBlog.Services;

namespace PortainerBlog.Features.Posts.Endpoints;

public class CreatePostEndpoint(IPostService postService)
    : Endpoint<CreatePostRequest, PostResponse>
{
    public override void Configure()
    {
        Post("/api/posts");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreatePostRequest req, CancellationToken ct)
    {
        PostResponse post = await postService.CreatePostAsync(req);
        await SendCreatedAtAsync<GetPostByIdEndpoint>(new { id = post.Id }, post, cancellation: ct);
    }
}
