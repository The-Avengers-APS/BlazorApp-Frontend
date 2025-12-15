namespace BlazorApp.Services.Auth;

using Blazored.LocalStorage;

public class TokenStorageService : ITokenStorageService
{
    private readonly ILocalStorageService _localStorage;

    private const string AccessTokenKey = "accessToken";
    private const string RefreshTokenKey = "refreshToken";
    private const string UserInfoKey = "userInfo";

    public TokenStorageService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        return await _localStorage.GetItemAsStringAsync(AccessTokenKey);
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        return await _localStorage.GetItemAsStringAsync(RefreshTokenKey);
    }

    public async Task SetTokensAsync(string accessToken, string refreshToken)
    {
        await _localStorage.SetItemAsStringAsync(AccessTokenKey, accessToken);
        await _localStorage.SetItemAsStringAsync(RefreshTokenKey, refreshToken);
    }

    public async Task ClearTokensAsync()
    {
        await _localStorage.RemoveItemAsync(AccessTokenKey);
        await _localStorage.RemoveItemAsync(RefreshTokenKey);
        await _localStorage.RemoveItemAsync(UserInfoKey);
    }

    public async Task<string?> GetUserInfoAsync()
    {
        return await _localStorage.GetItemAsStringAsync(UserInfoKey);
    }

    public async Task SetUserInfoAsync(string userInfoJson)
    {
        await _localStorage.SetItemAsStringAsync(UserInfoKey, userInfoJson);
    }
}