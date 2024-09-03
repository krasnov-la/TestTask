namespace TestTasks.Contracts.Doctor;

public record DoctorRequest(
    string FullName,
    Guid CabinetId,
    Guid SpecializationId,
    Guid? RegionId);