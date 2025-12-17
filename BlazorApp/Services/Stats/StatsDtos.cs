namespace BlazorApp.Services.Stats;

public record UserStatisticsDto(
    Guid UserId,
    string UserName,
    string Email,
    TrainingStatsDto Training,
    TeamSessionStatsDto TeamSessions,
    ProgramProgressDto Programs,
    int TotalCheckIns,
    List<Guid> AchievementIds,
    DateTime CreatedAt,
    DateTime LastUpdated);

public record TrainingStatsDto(
    int TotalSessions,
    int TotalMinutes,
    int TotalCaloriesBurned,
    double TotalDistanceKm,
    DateTime? LastSessionDate);

public record TeamSessionStatsDto(
    int TotalSessions,
    int TotalMinutes,
    DateTime? LastSessionDate);

public record ProgramProgressDto(
    int TotalCompleted,
    string? CurrentProgramName,
    DateTime? CurrentProgramStartDate,
    int CurrentProgramSessionsCompleted,
    int CurrentProgramTotalSessions);

public record AchievementDto(
    Guid UserId,
    string Title,
    string Description,
    string Type,
    DateTime UnlockedDate,
    string? IconUrl);

public record AchievementDefinitionDto(
    string Type,
    string Title,
    string Description,
    string IconUrl,
    int Threshold,
    string Category);

public record GrantAchievementRequest(Guid UserId, string AchievementType);
