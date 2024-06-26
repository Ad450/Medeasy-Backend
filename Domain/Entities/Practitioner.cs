namespace Domain.Entities;

public class Practitioner
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Kyc? Kyc { get; set; }
    public PractitionerProfilePicture? ProfilePicture { get; set; }
    public PractitionerLocation? Location { get; set; }
    public ICollection<PractitionerService> PractitionerServices { get; set; } = [];
    public ICollection<Service> Services { get; set; } = [];
    public ICollection<Appointment> Appointments { get; set; } = [];
    public ICollection<Day> Days { get; set; } = [];
}