namespace Domain.Entities;

public class PatientProfilePicture
{
    public Guid Id { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
}