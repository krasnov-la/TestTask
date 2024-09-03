namespace TestTasks.Domain;

public class Doctor
{ 
    public Guid Id = Guid.NewGuid();
    public string FullName { get; set; }
    public Guid CabinetId { get; set; }
    public Guid SpecializationId { get; set; }
    public Guid? RegionId { get; set; }
    public Cabinet? Cabinet { get; set; }
    public Specialization? Specialization { get; set; }
    public Region? Region { get; set; }

    public Doctor(string fullName,
                  Guid cabinetId,
                  Guid specializationId,
                  Guid? regionId)
    {
        FullName = fullName;
        CabinetId = cabinetId;
        SpecializationId = specializationId;
        RegionId = regionId;
    }

    private Doctor() { }
}