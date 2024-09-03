namespace TestTasks.Contracts.Patient;

public record PatientRequest(
    string FirstName,
    string MiddleName,
    string LastName,
    string Address,
    DateOnly DateOfBirth,
    string Sex,
    Guid RegionId
    );