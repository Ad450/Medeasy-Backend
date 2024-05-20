using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Age).IsRequired();

        builder.HasMany(x => x.Appointments).WithOne(a => a.Patient)
        .HasForeignKey(a => a.PatientId);

        builder.HasOne(x => x.ProfilePicture).WithOne(p => p.Patient)
        .HasForeignKey<PatientProfilePicture>(a => a.PatientId);
    }
}