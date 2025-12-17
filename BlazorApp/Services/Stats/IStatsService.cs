namespace BlazorApp.Services.Stats;

public interface IStatsService
{
    // User Statistics
    Task<UserStatisticsDto?> GetMyStatisticsAsync();
    Task<UserStatisticsDto?> GetUserStatisticsAsync(Guid userId);

    // Achievements
    Task<List<AchievementDto>> GetMyAchievementsAsync();
    Task<List<AchievementDto>> GetUserAchievementsAsync(Guid userId);
    Task<List<AchievementDefinitionDto>> GetAchievementDefinitionsAsync();
    Task<bool> GrantAchievementAsync(GrantAchievementRequest request);
}
