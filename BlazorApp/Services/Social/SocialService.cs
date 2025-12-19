using System.Net.Http.Json;

namespace BlazorApp.Services.Social;

public class SocialService : ISocialService
{
    private readonly HttpClient _httpClient;

    public SocialService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Search

    public async Task<List<UserProfileDto>> SearchUsersAsync(string query)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserProfileDto>>($"api/social/search?query={Uri.EscapeDataString(query)}");
            return result ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<PagedUsersDto> GetAllUsersAsync(int page = 1, int pageSize = 10)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<PagedUsersDto>($"api/social/users?page={page}&pageSize={pageSize}");
            return result ?? new PagedUsersDto([], 0, page, pageSize);
        }
        catch
        {
            return new PagedUsersDto([], 0, page, pageSize);
        }
    }

    // Friends

    public async Task<List<UserProfileDto>> GetFriendsAsync(Guid userId)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserProfileDto>>($"api/friends/{userId}");
            return result ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<UserProfileDto>> GetFriendsAtGymAsync(Guid userId)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserProfileDto>>($"api/social/{userId}/friends-at-gym");
            return result ?? [];
        }
        catch
        {
            return [];
        }
    }

    // Friend Requests

    public async Task<List<FriendRequestDto>> GetPendingRequestsAsync(Guid userId)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<FriendRequestDto>>($"api/friends/requests/pending?userId={userId}");
            return result ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> SendFriendRequestAsync(SendFriendRequestDto request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/friends/request", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AcceptFriendRequestAsync(string requestId)
    {
        try
        {
            var response = await _httpClient.PutAsync($"api/friends/requests/{requestId}/accept", null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RejectFriendRequestAsync(string requestId)
    {
        try
        {
            var response = await _httpClient.PutAsync($"api/friends/requests/{requestId}/reject", null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UnfriendAsync(Guid userId, Guid friendId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/friends/{userId}/{friendId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
