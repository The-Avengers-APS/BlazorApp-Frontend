namespace BlazorApp.Services.Booking;

public interface IBookingService
{
    /// <summary>
    /// Get all available bookings
    /// </summary>
    Task<List<BookingDto>> GetAllBookingsAsync();

    /// <summary>
    /// Get a specific booking by ID
    /// </summary>
    Task<BookingDto?> GetBookingByIdAsync(string id);

    /// <summary>
    /// Join a booking (add current user as participant)
    /// </summary>
    Task<bool> JoinBookingAsync(string bookingId);

    /// <summary>
    /// Leave a booking (remove current user from participants)
    /// </summary>
    Task<bool> LeaveBookingAsync(string bookingId);

    /// <summary>
    /// Create a new booking (admin only)
    /// </summary>
    Task<BookingDto?> CreateBookingAsync(BookingCreateDto createDto);

    /// <summary>
    /// Delete a booking (admin only)
    /// </summary>
    Task<bool> DeleteBookingAsync(string id);
}
