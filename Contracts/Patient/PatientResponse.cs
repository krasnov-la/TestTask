namespace TestTasks.Contracts.Patient;

public record PatientResponse(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string Address,
    DateOnly DateOfBirth,
    string Sex,
    Guid RegionId
    );