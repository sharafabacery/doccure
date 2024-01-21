using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace doccure.Data.Models
{
    public class Applicationuser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        [MaxLength(5)]
        public string? BloodGroup { get; set; }

        public string? Image { get; set; }
        public bool? Gender { get; set; }

        public Address? address { get; set; }
        public Doctor? doctor { get; set; }
		public ICollection<Booking> PatientBooking { get; } = new List<Booking>(); // Collection navigation containing dependents
		public ICollection<Booking> DoctorBooking { get; } = new List<Booking>(); // Collection navigation containing dependents


	}
}
