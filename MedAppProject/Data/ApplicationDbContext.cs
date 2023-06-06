using MedAppProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedAppProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasSequence<int>("UserIdSequence", schema: "shared")
                .StartsAt(100)
                .IncrementsBy(1);
            
            modelBuilder.Entity<Doctor>()
                .Property(d => d.DoctorRate)
                .HasDefaultValue(0);
            
            modelBuilder.Entity<Patient>()
            .Property(u => u.Id)
            .HasDefaultValueSql("NEXT VALUE FOR shared.UserIdSequence");
            modelBuilder.Entity<Lab>()
            .Property(u => u.Id)
            .HasDefaultValueSql("NEXT VALUE FOR shared.UserIdSequence");
            modelBuilder.Entity<Doctor>()
            .Property(u => u.Id)
            .HasDefaultValueSql("NEXT VALUE FOR shared.UserIdSequence");
            modelBuilder.Entity<Pharmacist>()
            .Property(u => u.Id)
            .HasDefaultValueSql("NEXT VALUE FOR shared.UserIdSequence");






        }
        public DbSet<DoctorAvailableTimes> DoctorAvailableTimes { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Pharmacist> Pharmacies { get; set; }
        public DbSet<Specialization> Specilazations { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<DoctorAppointment> DoctorAppointments { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<LabAppointment> LabAppointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<TestInfo> testLabInfos { get; set; }
        public DbSet<VMLogin> VMLogins { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}
