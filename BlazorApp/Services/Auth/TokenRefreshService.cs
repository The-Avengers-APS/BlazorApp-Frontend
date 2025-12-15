namespace BlazorApp.Services.Auth;

using System.IdentityModel.Tokens.Jwt;

public class TokenRefreshService : IDisposable
{
    private readonly ITokenStorageService _tokenStorage;
    private readonly IServiceProvider _serviceProvider;
    private Timer? _timer;

    private const int RefreshBeforeExpiryMinutes = 2;

    public TokenRefreshService(
        ITokenStorageService tokenStorage,
        IServiceProvider serviceProvider)
    {
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
    }

    public async void ScheduleRefresh()
    {
        _timer?.Dispose();
        _timer = null;

        try
        {
            var token = await _tokenStorage.GetAccessTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                return;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiresAt = jwtToken.ValidTo;
            var refreshAt = expiresAt.AddMinutes(-RefreshBeforeExpiryMinutes);
            var delay = refreshAt - DateTime.UtcNow;

            if (delay <= TimeSpan.Zero)
            {
                // Token already needs refresh
                await RefreshNow();
                return;
            }

            // Schedule single refresh
            _timer = new Timer(
                async _ => await RefreshNow(),
                null,
                delay,
                Timeout.InfiniteTimeSpan);
        }
        catch
        {
            // Invalid token, do nothing
        }
    }

    private async Task RefreshNow()
    {
        try
        {
            var authService = _serviceProvider.GetRequiredService<IAuthService>();
            var success = await authService.RefreshTokenAsync();

            if (success)
            {
                // Schedule next refresh for new token
                ScheduleRefresh();
            }
        }
        catch
        {
            // Refresh failed, user will be logged out on next request
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _timer = null;
    }
}
