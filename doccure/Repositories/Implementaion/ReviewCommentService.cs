using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	[Authorize]
	public class ReviewCommentService : IReviewCommentService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public ReviewCommentService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext) {
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Review> AddComment2Review(ClaimsPrincipal user, Comment comment)
		{
			var review=await applicationDbContext.Reviews.Include(c=>c.Comments).Include(b=>b.booking)
				.Where(c=>c.Id==comment.ReviewId).SingleOrDefaultAsync();
			if(review!=null)
			{
				comment.createdDate = DateTime.Now;
				
				review.Comments.Add(comment);
				var res = await applicationDbContext.SaveChangesAsync();
				if (res > 0)
				{
					return review;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
			
		}
	}
}
