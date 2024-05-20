using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PractitionerLocationEntityTypeConfiguration : IEntityTypeConfiguration<PractitionerLocation>
{
    public void Configure(EntityTypeBuilder<PractitionerLocation> builder)
    {
        builder.Property(x => x.LocationName).IsRequired();

        builder.HasOne(x => x.Practitioner).WithOne(p => p.Location)
        .HasForeignKey<PractitionerLocation>(a => a.PractitionerId);
    }
}