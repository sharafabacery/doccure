using System.ComponentModel.DataAnnotations.Schema;

namespace doccure.Data.ResponseModels
{
	[NotMapped]
	public class Patients
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Address { get; set; }
		public string? ProfilePicture { get; set; }
		public string? PhoneNumber { get; set; }
		public int Age { get; set; }
		public string BloodGroup { get; set; }
		public string Gender { get; set; }


	}
}
