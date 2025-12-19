using System.Net.Http.Json;
using BlazorApp.Services.Auth;
using Shared.Contracts.DTOs;

namespace BlazorApp.Services.User;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;

    public UserService(HttpClient httpClient, IAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    // ============== Body Metrics Operations ==============

    public async Task<UserMetricsDto?> GetMyMetricsAsync()
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null) return null;

            return await _httpClient.GetFromJsonAsync<UserMetricsDto>($"api/users/{user.Id}/metrics");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateMyMetricsAsync(UpdateBodyMetricsRequest request)
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null) return false;

            var response = await _httpClient.PutAsJsonAsync($"api/users/{user.Id}/metrics", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ============== Weight History Operations ==============

    public async Task<List<BodyMetricLogDto>> GetMyWeightHistoryAsync(int limit = 30)
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null) return [];

            var result = await _httpClient.GetFromJsonAsync<List<BodyMetricLogDto>>(
                $"api/users/{user.Id}/metrics/history?limit={limit}");
            return result ?? [];
        }
        catch
        {
            return [];
        }
    }
}
