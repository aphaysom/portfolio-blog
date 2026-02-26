using Mediator;
using Microsoft.Extensions.Caching.Hybrid;
using PortainerBlog.DTOs;
using PortainerBlog.Repositories;

namespace PortainerBlog.Features.Posts.Queries;

public record GetPostsQuery() : IRequest<IEnumerable<PostResponse>>;

#pragma warning disable EXTEXP0018
public class GetPostsHandler(IPostRepository repository, HybridCache cache)
    : IRequestHandler<GetPostsQuery, IEnumerable<PostResponse>>
{
    public async ValueTask<IEnumerable<PostResponse>> Handle(
        GetPostsQuery request,
        CancellationToken cancellationToken
    )
    {
        var posts = await cache.GetOrCreateAsync(
            "posts:all",
            async ct =>
            {
                var dbPosts = await repository.GetAllAsync();
                return dbPosts
                    .Select(p => new PostResponse(p.Id, p.Title, p.Content, p.CreatedAt))
                    .ToList();
            },
            new HybridCacheEntryOptions { Expiration = TimeSpan.FromSeconds(30) },
            tags: ["posts"],
            cancellationToken: cancellationToken
        );

        return posts;
    }
}
#pragma warning restore EXTEXP0018
