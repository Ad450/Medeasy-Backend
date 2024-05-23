namespace Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public PatientProfilePicture? ProfilePicture { get; set; }
    public PatientLocation? Location { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];
}