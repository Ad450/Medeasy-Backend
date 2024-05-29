namespace Domain.Entities;

public class Service
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<PractitionerService> PractitionerServices { get; set; } = [];
    public ICollection<Practitioner> Practitioners { get; set; } = [];
    public ICollection<Appointment> Appointments { get; set; } = [];
}