using HospitalManagement.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship between Patient and Appointment
            modelBuilder.Entity<AppointmentModel>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete appointments when patient is deleted

            modelBuilder.Entity<AppointmentModel>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent deletion of a doctor if appointments exist
        }

        public DbSet<PatientModel> Patients { get; set; }  // DbSet for Patient
        public DbSet<DoctorModel> Doctors { get; set; }    // DbSet for Doctor
        public DbSet<AppointmentModel> Appointments { get; set; }  // DbSet for Appointment
    }
}