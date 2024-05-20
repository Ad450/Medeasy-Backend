using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PatientProfilePictureEntityTypeConfiguration : IEntityTypeConfiguration<PatientProfilePicture>
{
    public void Configure(EntityTypeBuilder<PatientProfilePicture> builder)
    {
        builder.Property(x => x.PictureUrl).IsRequired();

        builder.HasOne(x => x.Patient).WithOne(p => p.ProfilePicture)
        .HasForeignKey<PatientProfilePicture>(a => a.PatientId);
    }
}