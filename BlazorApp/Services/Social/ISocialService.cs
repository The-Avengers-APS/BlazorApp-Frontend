namespace BlazorApp.Services.Social;

public interface ISocialService
{
    // Search
    Task<List<UserProfileDto>> SearchUsersAsync(string query);

    // All Users
    Task<PagedUsersDto> GetAllUsersAsync(int page = 1, int pageSize = 10);

    // Friends
    Task<List<UserProfileDto>> GetFriendsAsync(Guid userId);
    Task<List<UserProfileDto>> GetFriendsAtGymAsync(Guid userId);

    // Friend Requests
    Task<List<FriendRequestDto>> GetPendingRequestsAsync(Guid userId);
    Task<bool> SendFriendRequestAsync(SendFriendRequestDto request);
    Task<bool> AcceptFriendRequestAsync(string requestId);
    Task<bool> RejectFriendRequestAsync(string requestId);
    Task<bool> UnfriendAsync(Guid userId, Guid friendId);
}
