using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class ReviewService : IReviewService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public ReviewService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Review> AddReview(ClaimsPrincipal user,ReviewRequest reviewRequest)
		{
			var booking =await applicationDbContext.Bookings
													.Include(d => d.Review)
													.Include(d => d.doctor.doctor)
													.Where(e => e.Review == null && e.doctor.doctor.Id == reviewRequest.DoctorID && e.patientId == userManager.GetUserId(user))
													.FirstOrDefaultAsync();
			
			if(booking == null)
			{
				return null;
			}
			else
			{
				var review = new Review();
				review.stars = reviewRequest.stars;
				review.Title = reviewRequest.Title;
				review.Description=reviewRequest.Description;
				review.BookingId = booking.Id;
				review.createdDate = new DateTime();
				booking.Review = review;
				var Inserted = await applicationDbContext.SaveChangesAsync();
				if (Inserted > 0)
				{
					return review;
				}
				else
				{
					return null;
				}
			}
		}
	}
}
