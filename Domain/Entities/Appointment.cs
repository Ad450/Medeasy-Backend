
namespace Domain.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public AppointmentState? State { get; set; }
    public Guid PractitionerId { get; set; }
    public Practitioner? Practitioner { get; set; }
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }
    public Guid PatientId { get; set; }
    public Patient? Patient { get; set; }
    public Guid DayId { get; set; }
    public Day? Day { get; set; }
}