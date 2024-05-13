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
        public DateTime? CreatedTime { get; set; }

        public Address? address { get; set; }
        public Doctor? doctor { get; set; }
		public ICollection<Booking> PatientBooking { get; } = new List<Booking>(); // Collection navigation containing dependents
		public ICollection<Booking> DoctorBooking { get; } = new List<Booking>(); // Collection navigation containing dependents
		public ICollection<Favourites> PatientFavourites { get; } = new List<Favourites>(); // Collection navigation containing dependents
		public ICollection<UserConnected> UserConnecteds { get; } = new List<UserConnected>(); // Collection navigation containing dependents
		public ICollection<UserGroups> UserGroups { get; } = new List<UserGroups>(); // Collection navigation containing dependents
																					 //public ICollection<Review> DoctorPatientReview { get; } = new List<Review>(); // Collection navigation containing dependents
		public ICollection<Messages> SenderMessages { get; } = new List<Messages>(); // Collection navigation containing dependents
		public ICollection<Messages> ReceiverMessages { get; } = new List<Messages>(); // Collection navigation containing dependents


	}
}
