using TestTasks.Domain;

namespace TestTasks.Contracts.Doctor;

public record DoctorFullResponse(
    Guid Id,
    string FullName,
    Cabinet Cabinet,
    Specialization Specialization,
    Region? Region);