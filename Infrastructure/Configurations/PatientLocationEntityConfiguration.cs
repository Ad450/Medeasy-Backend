using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PatientLocationEntityTypeConfiguration : IEntityTypeConfiguration<PatientLocation>
{
    public void Configure(EntityTypeBuilder<PatientLocation> builder)
    {
        builder.Property(x => x.LocationName).IsRequired();

        builder.HasOne(x => x.Patient).WithOne(a => a.Location)
        .HasForeignKey<PatientLocation>(a => a.PatientId);
    }
}