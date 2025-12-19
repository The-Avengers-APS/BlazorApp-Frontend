namespace BlazorApp.Services.Exercise;

// Alot of admin methods, used for implementing admin feature. Kept for easy implentation but not made during this sprint. 
// This is a PoC
public interface IExerciseService
{
    // ============== Exercises ==============
    Task<List<ExerciseDto>> GetAllExercisesAsync();
    Task<ExerciseDto?> CreateExerciseAsync(CreateExerciseRequest request);
    Task<bool> UpdateExerciseAsync(Guid id, UpdateExerciseRequest request);
    Task<bool> DeleteExerciseAsync(Guid id);

    // ============== Workouts ==============
    Task<List<WorkoutDto>> GetAllWorkoutsAsync();
    Task<WorkoutDto?> CreateWorkoutAsync(CreateWorkoutRequest request);
    Task<bool> UpdateWorkoutAsync(Guid id, UpdateWorkoutRequest request);
    Task<bool> DeleteWorkoutAsync(Guid id);
    Task<bool> AddExerciseToWorkoutAsync(Guid workoutId, AddWorkoutExerciseRequest request);
    Task<bool> RemoveExerciseFromWorkoutAsync(Guid workoutId, Guid exerciseId);

    // ============== Training Programs ==============
    Task<List<TrainingProgramDto>> GetAllTrainingProgramsAsync();
    Task<TrainingProgramDto?> GetTrainingProgramByIdAsync(Guid id);
    Task<TrainingProgramDto?> CreateTrainingProgramAsync(CreateTrainingProgramRequest request);
    Task<bool> UpdateTrainingProgramAsync(Guid id, UpdateTrainingProgramRequest request);
    Task<bool> DeleteTrainingProgramAsync(Guid id);
    Task<bool> AddWorkoutToProgramAsync(Guid programId, AddProgramWorkoutRequest request);
    Task<bool> RemoveWorkoutFromProgramAsync(Guid programId, Guid workoutId);

    // ============== User Programs ==============
    Task<List<UserProgramDto>> GetUserProgramsAsync(Guid userId);
    Task<UserProgramDto?> EnrollInProgramAsync(EnrollProgramRequest request);
    Task<bool> UnenrollFromProgramAsync(Guid id);
    Task<bool> RestartProgramAsync(Guid userProgramId);

    // ============== User Workouts ==============
    Task<List<UserWorkoutDto>> GetUserWorkoutsForProgramAsync(Guid programId, Guid userId);
    Task<UserWorkoutDto?> StartUserWorkoutAsync(StartUserWorkoutRequest request);
    Task<bool> CompleteUserWorkoutAsync(Guid id);
    Task<bool> ToggleExerciseCompletionAsync(Guid userWorkoutId, Guid exerciseId, bool isCompleted);
}
