using System.Net.Http.Json;

namespace BlazorApp.Pages.Homescreen;

public class UserStreak
{
    public int CurrentStreak { get; set; }
    public int StreakGoal { get; set; }
    public DateTime LastCheckIn { get; set; }
    public bool CheckedInToday { get; set; }
}

public class TrainingPlan
{
    public Guid? ProgramId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ExerciseCount { get; set; }
    public int DurationMinutes { get; set; }
    public int CompletedExercises { get; set; }
    public int TotalExercises { get; set; }
    public bool HasActiveProgram => ProgramId.HasValue;
    public int ProgressPercentage => TotalExercises > 0 ? (CompletedExercises * 100) / TotalExercises : 0;
}

public class TeamSession
{
    public string Name { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public int AvailableSpots { get; set; }
}

public class GymOccupancy
{
    public int CurrentOccupancy { get; set; }
    public int MaxCapacity { get; set; } = 150;

    public string StatusText => CurrentOccupancy switch
    {
        <= 50 => "Roligt",
        <= 100 => "Travlt",
        _ => "Meget travlt"
    };

    public string StatusColor => CurrentOccupancy switch
    {
        <= 50 => "success",
        <= 100 => "warning",
        _ => "danger"
    };
}

public class QuickAction
{
    public string IconName { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Href { get; set; } = "#";
}

