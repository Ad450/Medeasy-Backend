using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.AppointmentState).IsRequired();

        builder.HasOne(a => a.Patient).WithMany(p => p.Appointments)
        .HasForeignKey(a => a.PatientId);

        builder.HasOne(a => a.Practitioner).WithMany(p => p.Appointments)
       .HasForeignKey(a => a.PractitionerId);

        builder.HasOne(a => a.Service).WithMany(s => s.Appointments)
        .HasForeignKey(a => a.ServiceId);

        builder.HasOne(a => a.AppointmentState).WithOne(s => s.Appointment)
        .HasForeignKey<AppointmentState>(a => a.AppointmentId);

    }
}