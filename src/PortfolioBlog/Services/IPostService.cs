using PortainerBlog.DTOs;

namespace PortainerBlog.Services;

public interface IPostService
{
    Task<IEnumerable<PostResponse>> GetPostsAsync();
    Task<PostResponse?> GetPostAsync(Guid id);
    Task<PostResponse> CreatePostAsync(CreatePostRequest request);
}
