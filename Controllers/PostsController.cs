using Microsoft.AspNetCore.Mvc;
using PortainerBlog.DTOs;
using PortainerBlog.Services;

namespace PortainerBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IPostService postService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts()
    {
        var posts = await postService.GetPostsAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostResponse>> GetPost(Guid id)
    {
        var post = await postService.GetPostAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<PostResponse>> CreatePost(CreatePostRequest request)
    {
        var post = await postService.CreatePostAsync(request);

        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }
}
