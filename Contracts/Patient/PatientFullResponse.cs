using TestTasks.Domain;

namespace TestTasks.Contracts.Patient;

public record PatientFullResponse(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string Address,
    DateOnly DateOfBirth,
    string Sex,
    Region Region
    );