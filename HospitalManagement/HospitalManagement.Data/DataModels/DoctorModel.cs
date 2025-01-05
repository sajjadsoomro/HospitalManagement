using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Data.DataModels
{
    public class DoctorModel
    {
        [Key]
        public int Id { get; set; }  // Primary key
        public string DoctorName { get; set; }
        public string Specialization { get; set; }  // E.g., Cardiologist, Neurologist
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }  // Optional
    }
}