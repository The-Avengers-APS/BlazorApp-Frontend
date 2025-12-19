namespace BlazorApp.Services.Social;

public record UserProfileDto(
    Guid UserId,
    string Email,
    bool IsCurrentlyCheckedIn,
    DateTime? LastCheckInTime,
    int CurrentStreak,
    int LongestStreak,
    int FriendsCount);

public record FriendRequestDto(
    string Id,
    Guid SenderId,
    string SenderEmail,
    Guid ReceiverId,
    string ReceiverEmail,
    string Status,
    DateTime CreatedAt,
    DateTime? RespondedAt);

public record SendFriendRequestDto(
    Guid SenderId,
    Guid ReceiverId);

public record PagedUsersDto(
    List<UserProfileDto> Users,
    int TotalCount,
    int Page,
    int PageSize);
