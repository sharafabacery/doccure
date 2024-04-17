using doccure.Data.Models;

namespace doccure.Data.RequestModels
{
	public class ReviewCommentRequest
	{
		public int DoctorID { get; set; }
		public string Description { get; set; }
		public int ReviewId { get; set; }
		public int ParentCommentId { get; set; }

		
	}
}
