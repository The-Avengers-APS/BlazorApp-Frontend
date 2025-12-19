namespace BlazorApp.Services.Booking;

/// <summary>
/// Response DTO for booking data from the API
/// </summary>
public record BookingDto(
    string Id,
    string Name,
    string Genre,
    Guid GymId,
    string GymName,
    int Capacity,
    DateTime TimeStart,
    DateTime TimeEnd,
    List<string> Participants
)
{
    public int AvailableSpots => Capacity - Participants.Count;
    public bool IsFull => AvailableSpots <= 0;
}

/// <summary>
/// Request DTO for join/leave operations
/// </summary>
public record AddParticipantDto(string UserId);

/// <summary>
/// Request DTO for creating new bookings (admin only)
/// </summary>
public record BookingCreateDto(
    string Name,
    string Genre,
    Guid GymId,
    string GymName,
    int Capacity,
    DateTime? TimeStart,
    DateTime? TimeEnd
);
