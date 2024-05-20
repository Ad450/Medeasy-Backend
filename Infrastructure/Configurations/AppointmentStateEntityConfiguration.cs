using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AppointmentStateEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentState>
{
    public void Configure(EntityTypeBuilder<AppointmentState> builder)
    {
        builder.Property(x => x.AppointmentStatus).IsRequired();

        builder.HasOne(s => s.Appointment).WithOne(a => a.AppointmentState)
        .HasForeignKey<AppointmentState>(s => s.AppointmentId);

    }
}