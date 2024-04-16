using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class ReviewDTO
	{
		public string FullName { get; set; }
		public string? Image { get; set; }
		public int BookingId { get; set; }
		public string Title { get; set; }
		public int stars { get; set; }
		public int DateDays { get; set; }
		public string Description { get; set; }
		public int Id { get; set; }
		public List<Comment> comments { get; set; }

	}
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
				review.createdDate = DateTime.Now;
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

		public async Task<List<ReviewDTO>> GetAllReviews(int DoctorId)
		{
			List<ReviewDTO> list =await applicationDbContext.Reviews
															.Include(c=>c.Comments)
															.Include(b=>b.booking)
															.Include(b=>b.booking.patient)
															.Include(b=>b.booking.doctor)
															.Include(b=>b.booking.doctor.doctor)
															.Where(b=>b.booking.doctor.doctor.Id==DoctorId)
															.Select(e=>new ReviewDTO { Image=e.booking.patient.Image,FullName=e.booking.patient.FirstName+ e.booking.patient.LastName, Id=e.Id,Title=e.Title,Description=e.Description,BookingId=e.BookingId,stars=e.stars,DateDays=(DateTime.Now-e.createdDate).Days,comments=e.Comments.ToList()})
															.ToListAsync();
			if(list.Count > 0)
			{
				return list;
			}
			{
				list = new List<ReviewDTO>();
				return list;
			}
			
		}
	}
}
