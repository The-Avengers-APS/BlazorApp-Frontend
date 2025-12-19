using Shared.Contracts.DTOs;

namespace BlazorApp.Services.User;

public interface IUserService
{
    // Body metrics operations
    Task<UserMetricsDto?> GetMyMetricsAsync();
    Task<bool> UpdateMyMetricsAsync(UpdateBodyMetricsRequest request);

    // Weight history operations
    Task<List<BodyMetricLogDto>> GetMyWeightHistoryAsync(int limit = 30);
}
