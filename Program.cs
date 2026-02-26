using Microsoft.EntityFrameworkCore;
using PortainerBlog.Data;
using PortainerBlog.Repositories;
using PortainerBlog.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

// Milestone 3: Minimal API Evolution
RouteGroupBuilder postsApi = app.MapGroup("/api/minimal/posts");

postsApi.MapPost(
    "/",
    async (CreatePostRequest request, IPostService postService) =>
    {
        PostResponse post = await postService.CreatePostAsync(request);
        return Results.Created($"/api/minimal/posts/{post.Id}", post);
    }
);

postsApi.MapGet(
    "/",
    async (IPostService postService) =>
    {
        IEnumerable<PostResponse> posts = await postService.GetPostsAsync();
        return Results.Ok(posts);
    }
);

postsApi.MapGet(
    "/{id}",
    async (Guid id, IPostService postService) =>
    {
        PostResponse? post = await postService.GetPostAsync(id);
        return post is not null ? Results.Ok(post) : Results.NotFound();
    }
);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.Run();
