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

    // ============== Profile Operations ==============

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"api/users/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null) return null;

            // Fetch fresh data from API (includes metrics)
            return await GetUserByIdAsync(user.Id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateProfileAsync(Guid id, UpdateProfileRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ChangePasswordAsync(Guid id, ChangeUserPasswordRequest request)
    {
        try
        {
            var apiRequest = new { OldPassword = request.OldPassword, NewPassword = request.NewPassword };
            var response = await _httpClient.PutAsJsonAsync($"api/users/{id}/password", apiRequest);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ============== Body Metrics Operations ==============

    public async Task<UserMetricsDto?> GetMetricsAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserMetricsDto>($"api/users/{id}/metrics");
        }
        catch
        {
            return null;
        }
    }

    public async Task<UserMetricsDto?> GetMyMetricsAsync()
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null) return null;

            return await GetMetricsAsync(user.Id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateMetricsAsync(Guid id, UpdateBodyMetricsRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/users/{id}/metrics", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateMyMetricsAsync(UpdateBodyMetricsRequest request)
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null) return false;

            return await UpdateMetricsAsync(user.Id, request);
        }
        catch
        {
            return false;
        }
    }

    // ============== Weight History Operations ==============

    public async Task<List<BodyMetricLogDto>> GetWeightHistoryAsync(Guid id, int limit = 30)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<BodyMetricLogDto>>(
                $"api/users/{id}/metrics/history?limit={limit}");
            return result ?? new List<BodyMetricLogDto>();
        }
        catch
        {
            return new List<BodyMetricLogDto>();
        }
    }

    public async Task<List<BodyMetricLogDto>> GetMyWeightHistoryAsync(int limit = 30)
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null) return new List<BodyMetricLogDto>();

            return await GetWeightHistoryAsync(user.Id, limit);
        }
        catch
        {
            return new List<BodyMetricLogDto>();
        }
    }

    public async Task<bool> LogWeightAsync(LogWeightRequest request)
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null) return false;

            var apiRequest = new
            {
                WeightKg = request.WeightKg,
                HeightCm = request.HeightCm,
                Notes = request.Notes
            };

            var response = await _httpClient.PostAsJsonAsync($"api/users/{user.Id}/metrics/history", apiRequest);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
