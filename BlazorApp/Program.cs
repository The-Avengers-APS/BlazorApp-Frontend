using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using BlazorApp;
using BlazorApp.Services.Auth;
using BlazorBootstrap;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// API Base URL - points to NGINX gateway
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:80";

// Add Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Register token storage service
builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();

// Register authentication state provider
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthenticationStateProvider>());

// Register authorization message handler
builder.Services.AddScoped<AuthorizationMessageHandler>();

// Configure HttpClient with authorization handler
builder.Services.AddScoped(sp =>
{
    var tokenStorage = sp.GetRequiredService<ITokenStorageService>();
    var handler = new AuthorizationMessageHandler(tokenStorage, sp);
    return new HttpClient(handler) { BaseAddress = new Uri(apiBaseUrl) };
});

// Register auth service
builder.Services.AddScoped<IAuthService, AuthService>();

// Register token refresh service for proactive token refresh
builder.Services.AddScoped<TokenRefreshService>();

// Add authorization
builder.Services.AddAuthorizationCore();

// BlazorBootstrap
builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();