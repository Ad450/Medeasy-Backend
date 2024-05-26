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
        .HasForeignKey(a => a.Practitioner);

        builder.HasMany(x => x.Days).WithOne(d => d.Practitioner)
        .HasForeignKey(d => d.Practitioner);

        builder.HasMany(x => x.Services).WithMany(a => a.Practitioners)
        .UsingEntity<PractitionerService>("PractitionerService");
    }
}