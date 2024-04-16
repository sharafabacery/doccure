using doccure.Data.Models;

namespace doccure.Data.RequestModels
{
	public class ReviewRequest
	{
		public int DoctorID { get; set; }
		public int stars { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Comment comment { get; set; }

	}
}
