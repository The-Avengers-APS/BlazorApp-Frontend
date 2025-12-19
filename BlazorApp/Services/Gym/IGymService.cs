namespace BlazorApp.Services.Gym;

public interface IGymService
{
    /// <summary>
    /// Get all gyms with their current occupancy
    /// </summary>
    Task<List<GymDto>> GetAllGymsAsync();

    /// <summary>
    /// Check in current user at a gym
    /// </summary>
    Task<CheckInResponse?> CheckInAsync(string gymName);
}
