using System.Net.Http.Headers;
using System.Net.Http.Json;
using PortfolioBlog.Web.Models;

namespace PortfolioBlog.Web.Services;

public class AuthTokenHandler(IHttpClientFactory httpClientFactory) : DelegatingHandler
{
    private string? _token;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        if (_token is null)
        {
            await AcquireTokenAsync(cancellationToken);
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        // If 401, try to re-authenticate once
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            await AcquireTokenAsync(cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            response = await base.SendAsync(request, cancellationToken);
        }

        return response;
    }

    private async Task AcquireTokenAsync(CancellationToken cancellationToken)
    {
        // Use a raw HttpClient to avoid circular dependency with Refit
        HttpClient client = httpClientFactory.CreateClient("AuthClient");
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequest("admin", "admin"),
            cancellationToken
        );

        response.EnsureSuccessStatusCode();
        LoginResponse? loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(
            cancellationToken
        );
        _token = loginResponse?.Token;
    }
}
