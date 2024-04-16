using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IReviewCommentService
	{
		public Task<Review> AddComment2Review(ClaimsPrincipal user, Comment comment);
	}
}
