using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class MedeasyUser : IdentityUser<Guid>
{
    public int Age { get; set; }
}