using System.Net.Http.Json;
using BlazorApp.Services.Auth;
using Shared.Contracts.DTOs;

namespace BlazorApp.Services.Notification;

public class NotificationService : INotificationService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;
    private readonly ILogger<NotificationService> _logger;

    private CancellationTokenSource? _pollingCts;
    private int _unreadCount;
    private bool _isPolling;

    private const int POLL_INTERVAL_MS = 30000; // 30 seconds

    public int UnreadCount => _unreadCount;

    public event Action? OnNotificationsChanged;

    public NotificationService(
        HttpClient httpClient,
        IAuthService authService,
        ILogger<NotificationService> logger)
    {
        _httpClient = httpClient;
        _authService = authService;
        _logger = logger;
    }
    
    public async Task<List<NotificationDto>> GetNotificationsAsync(int limit = 50)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<NotificationDto>>($"api/notifications?limit={limit}");
            return result ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to get notifications");
            return [];
        }
    }

    public async Task<int> GetUnreadCountAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<UnreadCountDto>("api/notifications/unread-count");
            return result?.Count ?? 0;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to get unread count");
            return 0;
        }
    }

    public async Task<bool> MarkAsReadAsync(string notificationId)
    {
        try
        {
            var response = await _httpClient.PutAsync($"api/notifications/{notificationId}/read", null);
            if (response.IsSuccessStatusCode)
            {
                await RefreshAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to mark notification as read: {NotificationId}", notificationId);
            return false;
        }
    }

    public async Task<bool> MarkAllAsReadAsync()
    {
        try
        {
            var response = await _httpClient.PutAsync("api/notifications/read-all", null);
            if (response.IsSuccessStatusCode)
            {
                _unreadCount = 0;
                OnNotificationsChanged?.Invoke();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to mark all notifications as read");
            return false;
        }
    }

    public async Task StartPollingAsync()
    {
        if (_isPolling) return;

        _isPolling = true;
        _pollingCts = new CancellationTokenSource();

        // Initial fetch
        await RefreshAsync();

        // Start background polling
        _ = PollNotificationsAsync(_pollingCts.Token);
    }

    public Task StopPollingAsync()
    {
        _isPolling = false;
        _pollingCts?.Cancel();
        _pollingCts?.Dispose();
        _pollingCts = null;
        return Task.CompletedTask;
    }

    public async Task RefreshAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        if (user == null) return;

        var newCount = await GetUnreadCountAsync();
        if (newCount != _unreadCount)
        {
            _unreadCount = newCount;
            OnNotificationsChanged?.Invoke();
        }
    }

    private async Task PollNotificationsAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(POLL_INTERVAL_MS, cancellationToken);

                if (cancellationToken.IsCancellationRequested) break;

                var user = await _authService.GetCurrentUserAsync();
                if (user == null) continue;

                var newCount = await GetUnreadCountAsync();
                if (newCount != _unreadCount)
                {
                    _unreadCount = newCount;
                    OnNotificationsChanged?.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error during notification polling");
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        await StopPollingAsync();
        GC.SuppressFinalize(this);
    }
}
