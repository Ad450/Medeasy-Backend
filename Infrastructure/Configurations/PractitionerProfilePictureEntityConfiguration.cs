using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PractitionerProfilePictureEntityTypeConfiguration : IEntityTypeConfiguration<PractitionerProfilePicture>
{
    public void Configure(EntityTypeBuilder<PractitionerProfilePicture> builder)
    {
        builder.Property(x => x.PictureUrl).IsRequired();

        builder.HasOne(x => x.Practitioner).WithOne(p => p.ProfilePicture)
        .HasForeignKey<PractitionerProfilePicture>(a => a.PractitionerId);
    }
}