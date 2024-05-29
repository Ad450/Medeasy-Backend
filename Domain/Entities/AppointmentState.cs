using Domain.Enum;

namespace Domain.Entities;


public class AppointmentState
{
    public Guid Id { get; set; }
    public AppointmentStatus AppointmentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }

};