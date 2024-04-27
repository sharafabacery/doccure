using doccure.Data;
using doccure.Repositories.Interfance;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class ReviewAdminDTO
	{
		public int Id { get; set; }
		public int Stars { get; set; }
		public string PatientName { get; set; }
		public string? PatienImage { get; set; }
		public string DoctortName { get; set; }
		public string ?DoctortImage { get; set; }
		public string Description { get; set; }
		public DateTime CreatedTime { get; set; }
	}
	public class ReviewListService : IReviewListService
	{
		private readonly ApplicationDbContext applicationDbContext;

		public ReviewListService(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}

		public async Task<bool> DeleteReview(int ReviewId)
		{
			var review =await applicationDbContext.Reviews.Where(e=>e.Id== ReviewId).FirstOrDefaultAsync();
			if(review != null)
			{
				review.Comments.Clear();
				applicationDbContext.Reviews.Remove(review);
				var res=await applicationDbContext.SaveChangesAsync();
				if (res > 0) return true;
				else { return false; }
			}
			else
			{
				return false;
			}

		}

		public async Task<List<ReviewAdminDTO>> GetAllReviews()
		{
			var reviews = await applicationDbContext.Reviews.Include(p => p.booking.patient).Include(d => d.booking.doctor)
							.OrderByDescending(e => e.createdDate).Select(e => new ReviewAdminDTO
							{
								Id=e.Id,
								PatientName=e.booking.patient.FirstName + " "+ e.booking.patient.LastName,
								PatienImage = e.booking.patient.Image,
								DoctortImage = e.booking.doctor.Image,
								DoctortName = e.booking.doctor.FirstName + " " + e.booking.doctor.LastName,
								Stars=e.stars,
								Description=e.Title+'\n'+e.Description,
								CreatedTime=e.createdDate
							}).ToListAsync();
			return reviews;
		}
	}
}
