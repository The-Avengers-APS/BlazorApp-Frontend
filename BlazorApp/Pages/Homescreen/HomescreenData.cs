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
    public string Name { get; set; } = string.Empty;
    public int ExerciseCount { get; set; }
    public int DurationMinutes { get; set; }
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

public class HomescreenData
{
    public UserStreak UserStreak { get; set; } = new();
    public TrainingPlan TodaysPlan { get; set; } = new();
    public TeamSession NextTeamSession { get; set; } = new();
    public GymOccupancy GymOccupancy { get; set; } = new();
    public List<QuickAction> QuickActions { get; set; } = new();
}

public class HomescreenDataService
{
    private readonly HttpClient _http;
    private HomescreenData? _cachedData;

    public HomescreenDataService(HttpClient http)
    {
        _http = http;
    }

    public async Task<HomescreenData> GetDataAsync()
    {
        if (_cachedData == null)
        {
            _cachedData = await _http.GetFromJsonAsync<HomescreenData>("sample-data/homescreen-data.json")
                          ?? new HomescreenData();
        }
        return _cachedData;
    }
}