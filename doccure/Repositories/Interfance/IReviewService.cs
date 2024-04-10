using doccure.Data.Models;
using doccure.Data.RequestModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IReviewService
	{
		public Task<Review> AddReview(ClaimsPrincipal user,ReviewRequest reviewRequest);
	}
}
