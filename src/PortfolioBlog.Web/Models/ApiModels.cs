namespace PortfolioBlog.Web.Models;

public record PostResponse(Guid Id, string Title, string Content, DateTime CreatedAt);

public record CreatePostRequest(string Title, string Content);

public record LoginRequest(string Username, string Password);

public record LoginResponse(string Token);
