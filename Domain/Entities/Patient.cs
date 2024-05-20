namespace Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }

    public PatientProfilePicture ProfilePicture { get; set; } = null!;
    public PatientLocation Location { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = [];
}