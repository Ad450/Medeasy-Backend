namespace Domain.Entities;

public class PractitionerLocation
{
    public Guid Id { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public double Lattitude { get; set; }
    public double Longitude { get; set; }
    public Guid PractitionerId { get; set; }
    public Practitioner Practitioner { get; set; } = null!;

};