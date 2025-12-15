namespace BlazorApp.Services.Auth;

using Shared.Contracts.DTOs;
using Shared.Contracts.Models.Auth;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string email, string password);
    Task<AuthResult> RegisterAsync(RegisterUserRequest request);
    Task LogoutAsync();
    Task<bool> RefreshTokenAsync();
    Task<UserDto?> GetCurrentUserAsync();
    Task<string?> GetCurrentRoleAsync();
    Task<string?> GetDisplayNameAsync();
    bool IsTokenExpired(string token);
}