namespace BlazorApp.Services.Auth;

using Shared.Contracts.Models;
using Shared.Contracts.Models.Auth;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string email, string password);
    Task<AuthResult> RegisterAsync(User user);
    Task LogoutAsync();
    Task<bool> RefreshTokenAsync();
    Task<User?> GetCurrentUserAsync();
    Task<string?> GetCurrentRoleAsync();
    Task<string?> GetDisplayNameAsync();
    bool IsTokenExpired(string token);
}