using System.Net.Http.Json;

namespace BlazorApp.Services.Stats;

public class StatsService : IStatsService
{
    private readonly HttpClient _httpClient;

    public StatsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // User Statistics

    public async Task<UserStatisticsDto?> GetMyStatisticsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserStatisticsDto>("api/userstatistics/me");
        }
        catch
        {
            return null;
        }
    }

    public async Task<UserStatisticsDto?> GetUserStatisticsAsync(Guid userId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserStatisticsDto>($"api/userstatistics/user/{userId}");
        }
        catch
        {
            return null;
        }
    }

    // Achievements

    public async Task<List<AchievementDto>> GetMyAchievementsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<AchievementDto>>("api/achievements/me") ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<AchievementDto>> GetUserAchievementsAsync(Guid userId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<AchievementDto>>($"api/achievements/user/{userId}") ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<AchievementDefinitionDto>> GetAchievementDefinitionsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<AchievementDefinitionDto>>("api/achievements/definitions") ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> GrantAchievementAsync(GrantAchievementRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/achievements/grant", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
