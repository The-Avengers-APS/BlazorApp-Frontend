namespace BlazorApp.Services.User;

public record UpdateBodyMetricsRequest(
    decimal? WeightKg,
    decimal? HeightCm,
    DateOnly? DateOfBirth,
    int? Gender,
    int? FitnessGoal,
    int? ActivityLevel
);
