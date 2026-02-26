using FastEndpoints;
using Mediator;
using PortainerBlog.DTOs;
using PortainerBlog.Features.Posts.Commands;

namespace PortainerBlog.Features.Posts.Endpoints;

public class CreatePostEndpoint(IMediator mediator) : Endpoint<CreatePostRequest, PostResponse>
{
    public override void Configure()
    {
        Post("/api/posts");
        Roles("Admin");
        Description(b => b.Produces<PostResponse>(201));
        Options(x => x.RequireRateLimiting("fixed")); // Corrected from x.Requirapp.UseRateLimiter();
    }

    public override async Task HandleAsync(CreatePostRequest req, CancellationToken ct)
    {
        PostResponse post = await mediator.Send(new CreatePostCommand(req.Title, req.Content), ct);
        await SendCreatedAtAsync<GetPostByIdEndpoint>(new { id = post.Id }, post, cancellation: ct);
    }
}
