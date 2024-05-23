using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class MedeasyUser : IdentityUser
{
    public int Age { get; set; }
}