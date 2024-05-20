namespace Domain.Entities;

public class PractitionerService
{
    public Guid PractitionerId { get; set; }
    public Practitioner Practitioner { get; set; } = null!;
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;


}