using PortfolioBlog.Web.Models;
using Refit;

namespace PortfolioBlog.Web.Services;

public interface IBlogApi
{
    [Get("/api/posts")]
    Task<List<PostResponse>> GetPostsAsync();

    [Get("/api/posts/{id}")]
    Task<PostResponse> GetPostByIdAsync(Guid id);

    [Post("/api/posts")]
    Task<PostResponse> CreatePostAsync([Body] CreatePostRequest request);

    [Post("/api/auth/login")]
    Task<LoginResponse> LoginAsync([Body] LoginRequest request);
}
