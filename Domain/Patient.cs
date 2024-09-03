namespace TestTasks.Domain;

public class Patient
{
    public Guid Id { get; set; } = Guid.NewGuid();  
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public DateOnly BirthDate { get; set; }
    public Sex Sex { get; set; }
    public Guid RegionId { get; set; }
    public Region? Region { get; set; }

    public Patient(string firstName,
                   string middleName,
                   string lastName,
                   string address,
                   DateOnly birthDate,
                   Sex sex,
                   Guid regionId)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Address = address;
        BirthDate = birthDate;
        Sex = sex;
        RegionId = regionId;
    }

    private Patient() { }
}

public enum Sex
{ 
    Male,
    Female
}