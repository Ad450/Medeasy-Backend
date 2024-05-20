using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class KycStatusEntityTypeConfiguration : IEntityTypeConfiguration<Kyc>
{
    public void Configure(EntityTypeBuilder<Kyc> builder)
    {
        builder.Property(x => x.KycStatus).IsRequired();

        builder.HasOne(s => s.Practitioner).WithOne(p => p.Kyc)
        .HasForeignKey<Kyc>(s => s.PractitionerId);

    }
}