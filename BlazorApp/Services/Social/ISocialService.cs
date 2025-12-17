namespace BlazorApp.Services.Social;

public interface ISocialService
{
    // Profile & Streak
    Task<UserProfileDto?> GetUserProfileAsync(Guid userId);
    Task<StreakDto?> GetUserStreakAsync(Guid userId);
    Task<List<UserProfileDto>> SearchUsersAsync(string query);

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
