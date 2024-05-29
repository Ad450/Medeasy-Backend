using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(x => x.Name).IsRequired();

        builder.HasMany(s => s.Practitioners)
             .WithMany(p => p.Services)
             .UsingEntity<PractitionerService>(
                 j => j.HasOne(ps => ps.Practitioner)
                       .WithMany(p => p.PractitionerServices)
                       .HasForeignKey(ps => ps.PractitionerId),
                 j => j.HasOne(ps => ps.Service)
                       .WithMany(s => s.PractitionerServices)
                       .HasForeignKey(ps => ps.ServiceId),
                 j =>
                 {
                     j.HasKey(ps => new { ps.PractitionerId, ps.ServiceId });
                 });
        builder.HasMany(s => s.Appointments).WithOne(a => a.Service)
       .HasForeignKey(a => a.ServiceId);

    }
}