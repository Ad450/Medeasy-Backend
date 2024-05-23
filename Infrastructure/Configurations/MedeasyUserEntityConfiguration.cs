using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MedeasyUserEntityTypeConfiguration : IEntityTypeConfiguration<MedeasyUser>
{
    public void Configure(EntityTypeBuilder<MedeasyUser> builder)
    {

    }
}