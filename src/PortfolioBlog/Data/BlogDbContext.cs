using Microsoft.EntityFrameworkCore;
using PortainerBlog.Models;

namespace PortainerBlog.Data;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options) { }

    public DbSet<Post> Posts { get; set; }
}
