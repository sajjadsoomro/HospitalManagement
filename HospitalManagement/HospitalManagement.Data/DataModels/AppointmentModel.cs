using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Data.DataModels
{
    public class AppointmentModel
    {
        [Key]
        public int Id { get; set; }  // Primary key

        [Required]
        [ForeignKey("PatientModel")]
        public int PatientId { get; set; }  // Foreign key to PatientModel

        [Required]
        [ForeignKey("DoctorModel")]
        public int DoctorId { get; set; }  // Foreign key to DoctorModel

        [Required]
        public DateTime AppointmentDateTime { get; set; }  // Date and time of the appointment

        public string? ReasonForVisit { get; set; }  // Optional reason for the appointment

        [Required]
        public AppointmentStatus Status { get; set; }  // Scheduled, Completed, Cancelled, etc.

        // Navigation properties (optional but helpful for relationships)
        public virtual PatientModel Patient { get; set; }
        public virtual DoctorModel Doctor { get; set; }
    }

    public enum AppointmentStatus
    {
        Scheduled = 1,
        Active = 2,
        Completed = 3,
        Cancelled = 4,
        Pending = 5
    }
}