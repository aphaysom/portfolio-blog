using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortainerBlog.Data;
using PortainerBlog.DTOs;
using PortainerBlog.Models;

namespace PortainerBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(BlogDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts()
    {
        var posts = await context
            .Posts.OrderByDescending(p => p.CreatedAt)
            .Select(p => new PostResponse(p.Id, p.Title, p.Content, p.CreatedAt))
            .ToListAsync();

        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostResponse>> GetPost(Guid id)
    {
        var post = await context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt));
    }

    [HttpPost]
    public async Task<ActionResult<PostResponse>> CreatePost(CreatePostRequest request)
    {
        var post = new Post { Title = request.Title, Content = request.Content };

        context.Posts.Add(post);
        await context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPost),
            new { id = post.Id },
            new PostResponse(post.Id, post.Title, post.Content, post.CreatedAt)
        );
    }
}
