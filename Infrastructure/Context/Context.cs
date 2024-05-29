using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Context;


// public class MedeasyContext(DbContextOptions<MedeasyContext> options) : IdentityDbContext<MedeasyUser, MedeasyRole, Guid>(options)
// {
//     public DbSet<Patient> Patients { get; set; }
//     public DbSet<Practitioner> Practitioners { get; set; }
//     public DbSet<Appointment> Appointments { get; set; }
//     public DbSet<AppointmentState> AppointmentStates { get; set; }
//     public DbSet<PatientLocation> PatientLocations { get; set; }
//     public DbSet<PatientProfilePicture> PatientProfilePictures { get; set; }
//     public DbSet<PractitionerProfilePicture> PractitionerProfilePictures { get; set; }
//     public DbSet<PractitionerLocation> PractitionerLocations { get; set; }
//     public DbSet<PractitionerService> PatientServices { get; set; }
//     public DbSet<Day> Days { get; set; }
//     public DbSet<Service> Services { get; set; }
//     public DbSet<Kyc> Kycs { get; set; }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);
//         modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentEntityTypeConfiguration).Assembly);
//     }

//     // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     // {
//     //     if (!optionsBuilder.IsConfigured)
//     //     {
//     //         optionsBuilder.UseNpgsql("user id = duncan; password = manu ; database = medeasy; host = localhost;");
//     //     }
//     // }
// }

public class MedeasyDbContext : IdentityDbContext<MedeasyUser, MedeasyRole, Guid>
{
    public MedeasyDbContext() { }
    public MedeasyDbContext(DbContextOptions<MedeasyDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MedeasyUser>(o => o.Property<Guid>("Id").HasColumnType("uuid"));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentEntityTypeConfiguration).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("user id = duncan; password = manu ; database = medeasy; host = localhost;");

        }
    }


    public DbSet<Patient> Patients { get; set; }
    public DbSet<Practitioner> Practitioners { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AppointmentState> AppointmentStates { get; set; }
    public DbSet<PatientLocation> PatientLocations { get; set; }
    public DbSet<PatientProfilePicture> PatientProfilePictures { get; set; }
    public DbSet<PractitionerProfilePicture> PractitionerProfilePictures { get; set; }
    public DbSet<PractitionerLocation> PractitionerLocations { get; set; }
    public DbSet<PractitionerService> PatientServices { get; set; }
    public DbSet<Day> Days { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Kyc> Kycs { get; set; }

}