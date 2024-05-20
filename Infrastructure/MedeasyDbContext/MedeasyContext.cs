using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.MeadeasyDbContext;


public class MedeasyContext : DbContext
{
    public MedeasyContext(DbContextOptions<MedeasyContext> options) : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentEntityTypeConfiguration).Assembly);
    }
}