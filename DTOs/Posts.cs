namespace PortainerBlog.DTOs;

public record PostResponse(Guid Id, string Title, string Content, DateTime CreatedAt);

public record CreatePostRequest(string Title, string Content);
