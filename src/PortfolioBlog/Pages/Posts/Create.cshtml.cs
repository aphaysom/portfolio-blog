using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortainerBlog.Data;
using PortainerBlog.Models;

namespace PortainerBlog.Pages.Posts;

public class CreateModel(BlogDbContext context) : PageModel
{
    [BindProperty]
    public Post Post { get; set; } = default!;

    public IActionResult OnGet()
    {
        Post = new();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Posts.Add(Post);
        await context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }
}
