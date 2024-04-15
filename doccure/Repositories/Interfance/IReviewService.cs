using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Implementaion;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IReviewService
	{
		public Task<Review> AddReview(ClaimsPrincipal user,ReviewRequest reviewRequest);
		public Task<List<ReviewDTO>> GetAllReviews(int DoctorId);

	}
}
