using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorApp;
using BlazorApp.Services.Auth;
using BlazorApp.Services.Booking;
using BlazorApp.Services.Exercise;
using BlazorApp.Services.Stats;
using BlazorApp.Services.Social;
using BlazorApp.Services.User;
using BlazorBootstrap;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Get API base URL from configuration
var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl") ?? "http://localhost:80";

// Add Blazored LocalStorage for token persistence
builder.Services.AddBlazoredLocalStorage();

// Add authorization
builder.Services.AddAuthorizationCore();

// Register auth services
builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<TokenRefreshService>();

// Register booking service
builder.Services.AddScoped<IBookingService, BookingService>();

// Register exercise service
builder.Services.AddScoped<IExerciseService, ExerciseService>();

// Register stats service
builder.Services.AddScoped<IStatsService, StatsService>();

// Register social service
builder.Services.AddScoped<ISocialService, SocialService>();

// Register user service
builder.Services.AddScoped<IUserService, UserService>();

// Register the authorization message handler
builder.Services.AddScoped<AuthorizationMessageHandler>();

// Configure HttpClient with base URL and auth handler
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthorizationMessageHandler>();
    return new HttpClient(handler) { BaseAddress = new Uri(apiBaseUrl) };
});

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();