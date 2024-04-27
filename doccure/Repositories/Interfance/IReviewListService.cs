using doccure.Repositories.Implementaion;

namespace doccure.Repositories.Interfance
{
	public interface IReviewListService
	{
		public Task<List<ReviewAdminDTO>> GetAllReviews();
		public Task<bool>DeleteReview(int ReviewId);
	}
}
