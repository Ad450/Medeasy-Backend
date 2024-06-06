using Domain.Entities;
using Infrastructure.Context;

namespace Medeasy_Backend.DatabaseSeedings;


public static class ServiceSeedings
{
    public static async Task InitService(MedeasyDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (context.Services.Any() || context.Roles.Any()) return;

        var services = new[]
        {
            new Service { Name = "Medicine" },
            new Service { Name = "Surgery" },
            new Service { Name = "Therapy" }
        };

        var roles = new[] {
            new MedeasyRole { Name = "Practitioner", NormalizedName= "PRACTITIONER"},
            new MedeasyRole { Name = "Patient", NormalizedName = "PATIENT"},
        };

        await context.Roles.AddRangeAsync(roles);

        await context.Services.AddRangeAsync(services);

        await context.SaveChangesAsync();
    }
}