namespace BlazorApp.Services.Gym;

/// <summary>
/// Response DTO for gym data from the API
/// </summary>
public record GymDto(
    Guid Id,
    string Name,
    string Location,
    int Capacity,
    int CurrentOccupancy,
    int MediumActivity,
    int HighActivity,
    TimeOnly Open,
    TimeOnly Close
)
{
    public int AvailableSpots => Capacity - CurrentOccupancy;
    public bool IsFull => AvailableSpots <= 0;
    public double OccupancyPercent => Capacity > 0 ? (double)CurrentOccupancy / Capacity * 100 : 0;

    public string StatusText => CurrentOccupancy switch
    {
        var o when o >= HighActivity => "Meget travlt",
        var o when o >= MediumActivity => "Travlt",
        _ => "Roligt"
    };

    public string StatusColor => CurrentOccupancy switch
    {
        var o when o >= HighActivity => "danger",
        var o when o >= MediumActivity => "warning",
        _ => "success"
    };
}

/// <summary>
/// Request DTO for check-in
/// </summary>
public record CheckInRequest(string Email, string GymName);

/// <summary>
/// Response DTO for check-in
/// </summary>
public record CheckInResponse(
    string Message,
    Guid Id,
    string Email,
    string GymName,
    DateTime CheckInTime
);
