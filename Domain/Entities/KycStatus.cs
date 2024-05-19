using Domain.Enum;

namespace Domain.Entities;


public class Kyc
{
    public Guid Id { get; set; }
    public KycStatus KycStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid PatientId { get; set; }
    public Practitioner Practitioner { get; set; } = null!;

};