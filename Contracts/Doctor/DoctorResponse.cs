namespace TestTasks.Contracts.Doctor;

public record DoctorResponse(
    Guid Id,
    string FullName,
    Guid CabinetId,
    Guid SpecializationId,
    Guid? RegionId);