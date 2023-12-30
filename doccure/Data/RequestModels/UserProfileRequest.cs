using doccure.Data.Models;

namespace doccure.Data.RequestModels
{
	public class UserProfileRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		public DateTime DateofBirth { get; set; }
		public string? BloodGroup { get; set; }
		public string? Image { get; set; }
		public IFormFile? ImageFile { get; set; }
		public string? PhoneNmber { get; set; }
		public Address Address1 { get; set; }
		
	}
}
