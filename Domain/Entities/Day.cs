
namespace Domain.Entities;

public class Day
{
    public int Guid { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int WeekNumber { get; set; }
    public Guid PractitionerId { get; set; }
    public Practitioner? Practitioner { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];

}