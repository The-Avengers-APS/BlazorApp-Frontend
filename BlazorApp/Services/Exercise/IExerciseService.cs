namespace BlazorApp.Services.Exercise;

public interface IExerciseService
{
    // ============== Exercises ==============
    Task<List<ExerciseDto>> GetAllExercisesAsync();
    Task<ExerciseDto?> GetExerciseByIdAsync(Guid id);
    Task<ExerciseDto?> CreateExerciseAsync(CreateExerciseRequest request);
    Task<bool> UpdateExerciseAsync(Guid id, UpdateExerciseRequest request);
    Task<bool> DeleteExerciseAsync(Guid id);

    // ============== Workouts ==============
    Task<List<WorkoutDto>> GetAllWorkoutsAsync();
    Task<WorkoutDto?> GetWorkoutByIdAsync(Guid id);
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
    Task<UserProgramDto?> GetUserProgramByIdAsync(Guid id);
    Task<UserProgramDto?> EnrollInProgramAsync(EnrollProgramRequest request);
    Task<bool> UpdateProgramProgressAsync(Guid id, UpdateProgramProgressRequest request);
    Task<bool> UnenrollFromProgramAsync(Guid id);

    // ============== User Workouts ==============
    Task<List<UserWorkoutDto>> GetUserWorkoutsAsync(Guid userId);
    Task<UserWorkoutDto?> GetUserWorkoutByIdAsync(Guid id);
    Task<List<UserWorkoutDto>> GetUserWorkoutsForProgramAsync(Guid programId, Guid userId);
    Task<UserWorkoutDto?> StartUserWorkoutAsync(StartUserWorkoutRequest request);
    Task<bool> UpdateUserWorkoutAsync(Guid id, UpdateUserWorkoutRequest request);
    Task<bool> CompleteUserWorkoutAsync(Guid id);
    Task<bool> DeleteUserWorkoutAsync(Guid id);
}
