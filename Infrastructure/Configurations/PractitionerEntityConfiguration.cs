using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PractitionerEntityTypeConfiguration : IEntityTypeConfiguration<Practitioner>
{
    public void Configure(EntityTypeBuilder<Practitioner> builder)
    {
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Age).IsRequired();

        builder.HasMany(x => x.Appointments).WithOne(a => a.Practitioner)
        .HasForeignKey(a => a.PractitionerId);

        builder.HasMany(x => x.Days).WithOne(d => d.Practitioner)
        .HasForeignKey(d => d.PractitionerId);

        builder.HasMany(x => x.Services)
               .WithMany(a => a.Practitioners)
               .UsingEntity<PractitionerService>(
                   j => j.HasOne(ps => ps.Service)
                         .WithMany(s => s.PractitionerServices)
                         .HasForeignKey(ps => ps.ServiceId),
                   j => j.HasOne(ps => ps.Practitioner)
                         .WithMany(p => p.PractitionerServices)
                         .HasForeignKey(ps => ps.PractitionerId),
                   j =>
                   {
                       j.HasKey(ps => new { ps.PractitionerId, ps.ServiceId });
                   });
    }
}