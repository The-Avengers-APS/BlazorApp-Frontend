namespace BlazorApp.Services.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Contracts.DTOs;
using Shared.Contracts.Models.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly TokenRefreshService _tokenRefreshService;

    public AuthService(
        HttpClient httpClient,
        ITokenStorageService tokenStorage,
        AuthenticationStateProvider authStateProvider,
        TokenRefreshService tokenRefreshService)
    {
        _httpClient = httpClient;
        _tokenStorage = tokenStorage;
        _authStateProvider = authStateProvider;
        _tokenRefreshService = tokenRefreshService;
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        try
        {
            var loginRequest = new LoginRequest { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);

            var result = await response.Content.ReadFromJsonAsync<AuthResult>();

            if (result?.Success == true && result.Tokens != null)
            {
                await _tokenStorage.SetTokensAsync(
                    result.Tokens.AccessToken,
                    result.Tokens.RefreshToken);

                if (result.User != null)
                {
                    await _tokenStorage.SetUserInfoAsync(JsonSerializer.Serialize(result.User));
                }

                ((JwtAuthenticationStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();

                // Schedule proactive token refresh
                _tokenRefreshService.ScheduleRefresh();
            }

            return result ?? AuthResult.Failed("Unknown error");
        }
        catch (Exception ex)
        {
            return AuthResult.Failed($"Login failed: {ex.Message}");
        }
    }

    public async Task<AuthResult> RegisterAsync(RegisterUserRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/register", request);

            if (response.IsSuccessStatusCode)
            {
                // Auto-login after successful registration
                return await LoginAsync(request.Email, request.Password);
            }

            var error = await response.Content.ReadAsStringAsync();
            return AuthResult.Failed(error);
        }
        catch (Exception ex)
        {
            return AuthResult.Failed($"Registration failed: {ex.Message}");
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var request = new RefreshTokenRequest { RefreshToken = refreshToken };
                await _httpClient.PostAsJsonAsync("api/auth/logout", request);
            }
        }
        catch
        {
            // Ignore logout errors for now - clear tokens anyway
        }
        finally
        {
            await _tokenStorage.ClearTokensAsync();
            ((JwtAuthenticationStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
        }
    }

    public async Task<bool> RefreshTokenAsync()
    {
        try
        {
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();
            if (string.IsNullOrEmpty(refreshToken))
            {
                return false;
            }

            var request = new RefreshTokenRequest { RefreshToken = refreshToken };
            var response = await _httpClient.PostAsJsonAsync("api/auth/refresh", request);

            if (!response.IsSuccessStatusCode)
            {
                await _tokenStorage.ClearTokensAsync();
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<AuthResult>();
            if (result?.Success == true && result.Tokens != null)
            {
                await _tokenStorage.SetTokensAsync(
                    result.Tokens.AccessToken,
                    result.Tokens.RefreshToken);
                return true;
            }

            await _tokenStorage.ClearTokensAsync();
            return false;
        }
        catch
        {
            await _tokenStorage.ClearTokensAsync();
            return false;
        }
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        var userInfoJson = await _tokenStorage.GetUserInfoAsync();
        if (string.IsNullOrEmpty(userInfoJson))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<UserDto>(userInfoJson);
        }
        catch
        {
            return null;
        }
    }

    public bool IsTokenExpired(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.ValidTo < DateTime.UtcNow;
        }
        catch
        {
            return true;
        }
    }

    public async Task<string?> GetCurrentRoleAsync()
    {
        return await ((JwtAuthenticationStateProvider)_authStateProvider).GetCurrentRoleAsync();
    }

    public async Task<string?> GetDisplayNameAsync()
    {
        return await ((JwtAuthenticationStateProvider)_authStateProvider).GetUserDisplayNameAsync();
    }
}