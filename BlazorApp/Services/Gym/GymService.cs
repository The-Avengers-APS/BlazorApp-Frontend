namespace BlazorApp.Services.Gym;

using System.Net.Http.Json;
using BlazorApp.Services.Auth;

public class GymService : IGymService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;

    public GymService(HttpClient httpClient, IAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<List<GymDto>> GetAllGymsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/gym");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GymDto>>()
                       ?? [];
            }

            return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<CheckInResponse?> CheckInAsync(string gymName)
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null)
            {
                return null;
            }

            var request = new CheckInRequest(user.Email, gymName);
            var response = await _httpClient.PostAsJsonAsync("api/gym/checkin", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CheckInResponse>();
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}
