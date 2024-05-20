using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(x => x.Name).IsRequired();

        builder.HasMany(s => s.Practitioners).WithMany(p => p.Services)
        .UsingEntity<PractitionerService>("PractitionerService");

        builder.HasMany(s => s.Appointments).WithOne(a => a.Service)
       .HasForeignKey(a => a.ServiceId);

    }
}