using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace HospitalManagement.Data.DataModels
{
    public class PatientModel
    {
        [Key]
        public int Id { get; set; }  // Primary key
        [Required]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }
        [Required]
        [Range(typeof(DateTime), "1/1/1900", "12/31/2023", ErrorMessage = "Date of Birth must be within the valid range.")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Email { get; set; } //can be optional
        public string? Address { get; set; }
        public string? MedicalHistory { get; set; }

        // Foreign key to DoctorModel
        [ForeignKey("DoctorModel")]
        public int DoctorId { get; set; }

        // Navigation property
        public virtual DoctorModel Doctor { get; set; }

        // Collection of appointments (one-to-many)
        public virtual ICollection<AppointmentModel> Appointments { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
