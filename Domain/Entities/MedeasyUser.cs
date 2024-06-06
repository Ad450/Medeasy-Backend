using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class MedeasyUser : IdentityUser<Guid>
{
    public int Age { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public string? RefreshToken { get; set; }
}