using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class DayEntityTypeConfiguration : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.Property(x => x.WeekNumber).IsRequired();
        builder.Property(x => x.DayOfWeek).IsRequired();

        builder.HasOne(d => d.Practitioner).WithMany(p => p.Days)
        .HasForeignKey(s => s.PractitionerId);

        builder.HasMany(d => d.Appointments).WithOne(a => a.Day)
        .HasForeignKey(a => a.DayId);

    }
}