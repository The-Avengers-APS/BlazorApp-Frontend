namespace BlazorApp.Services.User;

public record UpdateProfileRequest(
    string? GivenName,
    string? FamilyName,
    string? Telephone
);

public record UpdateBodyMetricsRequest(
    decimal? WeightKg,
    decimal? HeightCm,
    DateOnly? DateOfBirth,
    int? Gender,
    int? FitnessGoal,
    int? ActivityLevel
);

public record LogWeightRequest(
    decimal WeightKg,
    decimal? HeightCm,
    string? Notes
);

public record ChangeUserPasswordRequest(
    string OldPassword,
    string NewPassword
);
