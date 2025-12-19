namespace BlazorApp.Services.Stats;

public interface IStatsService
{
    // User Statistics
    Task<UserStatisticsDto?> GetMyStatisticsAsync();

    // Activity History (unified: check-ins, workouts, team sessions)
    Task<List<ActivityRecordDto>> GetMyActivitiesAsync(int days = 90);

    // Achievements
    Task<List<AchievementDto>> GetMyAchievementsAsync();
    Task<List<AchievementDefinitionDto>> GetAchievementDefinitionsAsync();
}
