using FastEndpoints;
using PortainerBlog.Infrastructure.Auth;

namespace PortainerBlog.Features.Auth;

public class LoginEndpoint(ITokenService tokenService) : Endpoint<LoginRequest, LoginResponse>
{
    public override void Configure()
    {
        Post("/api/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        // For demo purposes: hardcoded credentials
        // In production, this would validate against a user store (Identity, DB, etc.)
        if (req.Username != "admin" || req.Password != "admin")
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        string token = tokenService.GenerateToken(req.Username);
        await SendAsync(new LoginResponse(token), cancellation: ct);
    }
}
