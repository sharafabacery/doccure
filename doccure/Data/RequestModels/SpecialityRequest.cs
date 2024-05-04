using doccure.Data.Models;

namespace doccure.Data.RequestModels
{
	public class SpecialityRequest
	{
		public int Id { get; set; }
		public Speciality Speciality { get; set; }
		public IFormFile? ImageFile { get; set; }
	}
}
