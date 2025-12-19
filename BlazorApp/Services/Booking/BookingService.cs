namespace BlazorApp.Services.Booking;

using System.Net.Http.Json;
using BlazorApp.Services.Auth;

public class BookingService : IBookingService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;

    public BookingService(HttpClient httpClient, IAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<List<BookingDto>> GetAllBookingsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/bookings");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<BookingDto>>()
                       ?? new List<BookingDto>();
            }

            return new List<BookingDto>();
        }
        catch
        {
            return new List<BookingDto>();
        }
    }

    public async Task<bool> JoinBookingAsync(string bookingId)
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null)
            {
                return false;
            }

            var participantDto = new AddParticipantDto(user.Id.ToString());
            var response = await _httpClient.PostAsJsonAsync(
                $"api/bookings/{bookingId}/join",
                participantDto);

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> LeaveBookingAsync(string bookingId)
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null)
            {
                return false;
            }

            var participantDto = new AddParticipantDto(user.Id.ToString());
            var response = await _httpClient.PostAsJsonAsync(
                $"api/bookings/{bookingId}/leave",
                participantDto);

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<BookingDto?> CreateBookingAsync(BookingCreateDto createDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/bookings", createDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BookingDto>();
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteBookingAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/bookings/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
