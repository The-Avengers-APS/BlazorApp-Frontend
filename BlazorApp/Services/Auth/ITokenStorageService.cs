namespace BlazorApp.Services.Auth;

public interface ITokenStorageService
{
    Task<string?> GetAccessTokenAsync();
    Task<string?> GetRefreshTokenAsync();
    Task SetTokensAsync(string accessToken, string refreshToken);
    Task ClearTokensAsync();
    Task<string?> GetUserInfoAsync();
    Task SetUserInfoAsync(string userInfoJson);
}