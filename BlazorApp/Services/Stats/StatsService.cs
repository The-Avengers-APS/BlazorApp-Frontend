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

    // Activity History (unified)

    public async Task<List<ActivityRecordDto>> GetMyActivitiesAsync(int days = 90)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ActivityRecordDto>>($"api/userstatistics/me/activities?days={days}") ?? [];
        }
        catch
        {
            return [];
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
}
