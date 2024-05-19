namespace Domain.Entities;
public class PractitionerProfilePicture
{
    public Guid Id { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid PractitionerId { get; set; }
    public Practitioner Practitioner { get; set; } = null!;
}