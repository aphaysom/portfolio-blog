using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortainerBlog.Data;
using PortainerBlog.Models;

namespace PortainerBlog.Pages;

public class IndexModel(BlogDbContext context) : PageModel
{
    public IList<Post> Posts { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Posts = await context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }
}
