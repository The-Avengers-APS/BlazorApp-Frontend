namespace BlazorApp.Services.Social;

public record UserProfileDto(
    Guid UserId,
    string UserName,
    string Email,
    bool IsCurrentlyCheckedIn,
    DateTime? LastCheckInTime,
    int CurrentStreak,
    int LongestStreak,
    int FriendsCount);

public record FriendRequestDto(
    string Id,
    Guid SenderId,
    string SenderUserName,
    Guid ReceiverId,
    string ReceiverUserName,
    string Status,
    DateTime CreatedAt,
    DateTime? RespondedAt);

public record SendFriendRequestDto(
    Guid SenderId,
    Guid ReceiverId);

public record StreakDto(
    int CurrentStreak,
    int LongestStreak,
    DateTime? LastCheckInDate);
