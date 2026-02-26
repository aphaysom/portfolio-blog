using PortfolioBlog.Web.Components;
using PortfolioBlog.Web.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// Auth: raw HttpClient for token acquisition (avoids circular dependency)
builder.Services.AddHttpClient("AuthClient", c => c.BaseAddress = new Uri("http://localhost:5196"));

// Auth: DelegatingHandler that auto-injects Bearer tokens
builder.Services.AddTransient<AuthTokenHandler>();

// Milestone 9: Refit typed HTTP client with auth pipeline
builder
    .Services.AddRefitClient<IBlogApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5196"))
    .AddHttpMessageHandler<AuthTokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
