using Shared.Contracts.DTOs;

namespace BlazorApp.Services.User;

public interface IUserService
{
    // Profile operations
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto?> GetCurrentUserAsync();
    Task<bool> UpdateProfileAsync(Guid id, UpdateProfileRequest request);
    Task<bool> ChangePasswordAsync(Guid id, ChangeUserPasswordRequest request);

    // Body metrics operations
    Task<UserMetricsDto?> GetMetricsAsync(Guid id);
    Task<UserMetricsDto?> GetMyMetricsAsync();
    Task<bool> UpdateMetricsAsync(Guid id, UpdateBodyMetricsRequest request);
    Task<bool> UpdateMyMetricsAsync(UpdateBodyMetricsRequest request);

    // Weight history operations
    Task<List<BodyMetricLogDto>> GetWeightHistoryAsync(Guid id, int limit = 30);
    Task<List<BodyMetricLogDto>> GetMyWeightHistoryAsync(int limit = 30);
    Task<bool> LogWeightAsync(LogWeightRequest request);
}
