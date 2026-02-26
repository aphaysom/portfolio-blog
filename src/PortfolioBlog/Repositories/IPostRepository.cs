using PortainerBlog.Models;

namespace PortainerBlog.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(Guid id);
    Task AddAsync(Post post);
    Task SaveChangesAsync();
}
