using Shared.Contracts.DTOs;

namespace BlazorApp.Services.Notification;

public interface INotificationService : IAsyncDisposable
{
    /// <summary>
    /// Gets all notifications for the current user
    /// </summary>
    Task<List<NotificationDto>> GetNotificationsAsync(int limit = 50);

    /// <summary>
    /// Gets the count of unread notifications
    /// </summary>
    Task<int> GetUnreadCountAsync();

    /// <summary>
    /// Marks a specific notification as read
    /// </summary>
    Task<bool> MarkAsReadAsync(string notificationId);

    /// <summary>
    /// Marks all notifications as read
    /// </summary>
    Task<bool> MarkAllAsReadAsync();

    /// <summary>
    /// Current unread count (cached from last poll)
    /// </summary>
    int UnreadCount { get; }

    /// <summary>
    /// Event fired when notifications change (new notifications or read status changes)
    /// </summary>
    event Action? OnNotificationsChanged;

    /// <summary>
    /// Starts background polling for notifications
    /// </summary>
    Task StartPollingAsync();

    /// <summary>
    /// Stops background polling
    /// </summary>
    Task StopPollingAsync();

    /// <summary>
    /// Manually refreshes the notification count
    /// </summary>
    Task RefreshAsync();
}
