using System.Net.Http.Json;

namespace BlazorApp.Services.Exercise;

public class ExerciseService : IExerciseService
{
    private readonly HttpClient _httpClient;

    public ExerciseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // ============== Exercises ==============

    public async Task<List<ExerciseDto>> GetAllExercisesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/exercise");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ExerciseDto>>() ?? [];
            }
            return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<ExerciseDto?> GetExerciseByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ExerciseDto>($"api/exercise/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<ExerciseDto?> CreateExerciseAsync(CreateExerciseRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/exercise", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ExerciseDto>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateExerciseAsync(Guid id, UpdateExerciseRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/exercise/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteExerciseAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/exercise/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ============== Workouts ==============

    public async Task<List<WorkoutDto>> GetAllWorkoutsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/workout");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<WorkoutDto>>() ?? [];
            }
            return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<WorkoutDto?> GetWorkoutByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<WorkoutDto>($"api/workout/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<WorkoutDto?> CreateWorkoutAsync(CreateWorkoutRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/workout", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<WorkoutDto>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateWorkoutAsync(Guid id, UpdateWorkoutRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/workout/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteWorkoutAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/workout/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AddExerciseToWorkoutAsync(Guid workoutId, AddWorkoutExerciseRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"api/workout/{workoutId}/exercise", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveExerciseFromWorkoutAsync(Guid workoutId, Guid exerciseId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/workout/{workoutId}/exercise/{exerciseId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ============== Training Programs ==============

    public async Task<List<TrainingProgramDto>> GetAllTrainingProgramsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/trainingprogram");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TrainingProgramDto>>() ?? [];
            }
            return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<TrainingProgramDto?> GetTrainingProgramByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<TrainingProgramDto>($"api/trainingprogram/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<TrainingProgramDto?> CreateTrainingProgramAsync(CreateTrainingProgramRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/trainingprogram", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TrainingProgramDto>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateTrainingProgramAsync(Guid id, UpdateTrainingProgramRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/trainingprogram/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteTrainingProgramAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/trainingprogram/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AddWorkoutToProgramAsync(Guid programId, AddProgramWorkoutRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"api/trainingprogram/{programId}/workout", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveWorkoutFromProgramAsync(Guid programId, Guid workoutId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/trainingprogram/{programId}/workout/{workoutId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ============== User Programs ==============

    public async Task<List<UserProgramDto>> GetUserProgramsAsync(Guid userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/user/program?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserProgramDto>>() ?? [];
            }
            return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<UserProgramDto?> GetUserProgramByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserProgramDto>($"api/user/program/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<UserProgramDto?> EnrollInProgramAsync(EnrollProgramRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/program", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserProgramDto>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateProgramProgressAsync(Guid id, UpdateProgramProgressRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/user/program/{id}/progress", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UnenrollFromProgramAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/user/program/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ============== User Workouts ==============

    public async Task<List<UserWorkoutDto>> GetUserWorkoutsAsync(Guid userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/user/workout?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserWorkoutDto>>() ?? [];
            }
            return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<UserWorkoutDto?> GetUserWorkoutByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserWorkoutDto>($"api/user/workout/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<UserWorkoutDto>> GetUserWorkoutsForProgramAsync(Guid programId, Guid userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/user/workout/program/{programId}?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserWorkoutDto>>() ?? [];
            }
            return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<UserWorkoutDto?> StartUserWorkoutAsync(StartUserWorkoutRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/workout", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserWorkoutDto>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateUserWorkoutAsync(Guid id, UpdateUserWorkoutRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/user/workout/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> CompleteUserWorkoutAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.PostAsync($"api/user/workout/{id}/complete", null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteUserWorkoutAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/user/workout/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
