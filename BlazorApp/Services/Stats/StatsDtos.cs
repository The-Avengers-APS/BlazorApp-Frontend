namespace BlazorApp.Services.Stats;

public record UserStatisticsDto(
    Guid UserId,
    TrainingStatsDto Training,
    TeamSessionStatsDto TeamSessions,
    ProgramProgressDto Programs,
    int TotalCheckIns,
    int CurrentStreak,
    int LongestStreak,
    DateTime? LastCheckInDate,
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
    int TotalCompleted);

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

public record ActivityRecordDto(
    Guid Id,
    string Type,
    DateTime ActivityDate,
    string Title,
    string? Description,
    Guid? WorkoutId,
    string? BookingId,
    Guid? ProgramId,
    DateTime? BookingTimeEnd,
    bool IsAttended);
