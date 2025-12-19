namespace BlazorApp.Services.Auth;

using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;

public class AuthorizationMessageHandler : DelegatingHandler
{
    private readonly ITokenStorageService _tokenStorage;
    private readonly IServiceProvider _serviceProvider;
    private bool _isRefreshing;

    public AuthorizationMessageHandler(
        ITokenStorageService tokenStorage,
        IServiceProvider serviceProvider)
    {
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
        InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetAccessTokenAsync();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized && !_isRefreshing)
        {
            _isRefreshing = true;
            try
            {
                var authService = _serviceProvider.GetRequiredService<IAuthService>();
                var refreshed = await authService.RefreshTokenAsync();

                if (refreshed)
                {
                    var newToken = await _tokenStorage.GetAccessTokenAsync();
                    var newRequest = await CloneRequestAsync(request);
                    newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                    response = await base.SendAsync(newRequest, cancellationToken);
                }
                else
                {
                    // Refresh failed - clear tokens and notify auth state
                    await _tokenStorage.ClearTokensAsync();
                    var authStateProvider = _serviceProvider.GetRequiredService<AuthenticationStateProvider>();
                    ((JwtAuthenticationStateProvider)authStateProvider).NotifyAuthenticationStateChanged();
                }
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        return response;
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        if (request.Content != null)
        {
            var content = await request.Content.ReadAsStringAsync();
            clone.Content = new StringContent(content, System.Text.Encoding.UTF8,
                request.Content.Headers.ContentType?.MediaType ?? "application/json");
        }

        foreach (var header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }
}