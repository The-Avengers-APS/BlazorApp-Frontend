namespace BlazorApp.Services.Exercise;

// ============== Enums ==============

public enum Difficulty
{
    Begynder,
    Intermediær,
    Avanceret
}

// ============== Exercise DTOs ==============

public record ExerciseDto(
    Guid Id,
    string Name,
    string? MuscleGroup,
    List<string>? Equipment,
    int DefaultSets,
    string? DefaultReps,
    string? RestSeconds = null,
    string? RirTarget = null,
    string? Tempo = null,
    string? Notes = null,
    Difficulty? Difficulty = null,
    List<string>? CoachingCues = null,
    List<string>? CommonMistakes = null
);

public record CreateExerciseRequest(
    string Name,
    string? MuscleGroup,
    List<string>? Equipment,
    int DefaultSets,
    string? DefaultReps,
    string? RestSeconds = null,
    string? RirTarget = null,
    string? Tempo = null,
    string? Notes = null,
    Difficulty? Difficulty = null,
    List<string>? CoachingCues = null,
    List<string>? CommonMistakes = null
);

public record UpdateExerciseRequest(
    string Name,
    string? MuscleGroup,
    List<string>? Equipment,
    int DefaultSets,
    string? DefaultReps,
    string? RestSeconds = null,
    string? RirTarget = null,
    string? Tempo = null,
    string? Notes = null,
    Difficulty? Difficulty = null,
    List<string>? CoachingCues = null,
    List<string>? CommonMistakes = null
);

// ============== Workout DTOs ==============

public record WorkoutDto(
    Guid Id,
    string Title,
    string? Description,
    string? Category,
    string? Focus,
    List<WorkoutExerciseDto> Exercises
);

public record WorkoutExerciseDto(
    Guid ExerciseId,
    int OrderIndex,
    int DefaultSets,
    string? DefaultReps,
    string? RestSeconds = null,
    string? RirTarget = null,
    string? Tempo = null,
    string? Notes = null
);

public record CreateWorkoutRequest(
    string Title,
    string? Description,
    string? Category,
    string? Focus = null
);

public record UpdateWorkoutRequest(
    string Title,
    string? Description,
    string? Category,
    string? Focus = null
);

public record AddWorkoutExerciseRequest(
    Guid ExerciseId,
    int OrderIndex,
    int DefaultSets,
    string? DefaultReps,
    string? RestSeconds = null,
    string? RirTarget = null,
    string? Tempo = null,
    string? Notes = null
);

// ============== Training Program DTOs ==============

public record ProgramDurationDto(
    int Weeks,
    int WorkoutsPerWeek,
    int TotalWorkouts
);

public record WeightIncreaseGuidanceDto(
    string? UpperBody,
    string? LowerBody
);

public record ProgressionRulesDto(
    string? Intensity,
    string? DoubleProgression,
    WeightIncreaseGuidanceDto? WeightIncreaseGuidance,
    string? DeloadInfo
);

public record GeneralGuidelinesDto(
    string? Warmup,
    string? RestPeriods,
    string? TechniqueNotes,
    string? Notes
);

public record RecommendedScheduleEntryDto(
    string? Day,
    string? WorkoutId
);

public record TrainingProgramDto(
    Guid Id,
    string Title,
    string? Description,
    Difficulty Difficulty,
    int DurationWeeks,
    int TotalWorkouts,
    List<ProgramWorkoutDto> Workouts,
    ProgramDurationDto? Duration = null,
    string? LevelBadge = null,
    string? ShortDescription = null,
    List<string>? Goals = null,
    string? EstimatedTimePerWorkout = null,
    List<RecommendedScheduleEntryDto>? RecommendedSchedule = null,
    ProgressionRulesDto? ProgressionRules = null,
    GeneralGuidelinesDto? GeneralGuidelines = null
);

public record ProgramWorkoutDto(
    Guid WorkoutId,
    int OrderIndex,
    string? DayOfWeek
);

public record CreateTrainingProgramRequest(
    string Title,
    string? Description,
    Difficulty Difficulty,
    int DurationWeeks,
    int TotalWorkouts,
    ProgramDurationDto? Duration = null,
    string? LevelBadge = null,
    string? ShortDescription = null,
    List<string>? Goals = null,
    string? EstimatedTimePerWorkout = null,
    List<RecommendedScheduleEntryDto>? RecommendedSchedule = null,
    ProgressionRulesDto? ProgressionRules = null,
    GeneralGuidelinesDto? GeneralGuidelines = null
);

public record UpdateTrainingProgramRequest(
    string Title,
    string? Description,
    Difficulty Difficulty,
    int DurationWeeks,
    int TotalWorkouts,
    ProgramDurationDto? Duration = null,
    string? LevelBadge = null,
    string? ShortDescription = null,
    List<string>? Goals = null,
    string? EstimatedTimePerWorkout = null,
    List<RecommendedScheduleEntryDto>? RecommendedSchedule = null,
    ProgressionRulesDto? ProgressionRules = null,
    GeneralGuidelinesDto? GeneralGuidelines = null
);

public record AddProgramWorkoutRequest(
    Guid WorkoutId,
    int OrderIndex,
    string? DayOfWeek
);

// ============== User Program DTOs ==============

public enum ProgramStatus
{
    NotStarted,
    Active,
    Completed
}

public record UserProgramDto(
    Guid Id,
    Guid UserId,
    Guid ProgramId,
    DateTime StartedAt,
    ProgramStatus Status,
    int CurrentWeek,
    DateTime? CompletedAt,
    int CompletionCount
);

public record EnrollProgramRequest(
    Guid UserId,
    Guid ProgramId
);

// ============== User Workout DTOs ==============

public record UserWorkoutDto(
    Guid Id,
    Guid UserId,
    Guid? ProgramId,
    Guid WorkoutId,
    DateTime Date,
    bool IsCompleted,
    DateTime? CompletedAt,
    List<UserWorkoutExerciseDto> Exercises
);

public record UserWorkoutExerciseDto(
    Guid ExerciseId,
    int OrderIndex,
    decimal WeightKg,
    int TargetReps,
    bool IsCompleted
);

public record StartUserWorkoutRequest(
    Guid UserId,
    Guid? ProgramId,
    Guid WorkoutId,
    List<UserWorkoutExerciseInput>? Exercises
);

public record UserWorkoutExerciseInput(
    Guid ExerciseId,
    int OrderIndex,
    decimal WeightKg,
    int TargetReps
);
