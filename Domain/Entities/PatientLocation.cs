namespace Domain.Entities;

public class PatientLocation
{
    public Guid Id { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public double Lattitude { get; set; }
    public double Longitude { get; set; }
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

}