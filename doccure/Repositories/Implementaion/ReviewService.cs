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
		public List<CommentDTO> CommentDTO { get; set; } = new List<CommentDTO>();

	}
	public class CommentDTO
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public DateTime createdDate { get; set; }
		public string FullName { get; set; }
		public string? Image { get; set; }

		public int ReviewId { get; set; }
		public int? ParentCommentId { get; set; }
		public List<CommentDTO> CommentDTO2 { get; set; }

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
		public List<CommentDTO> GetAllDescendants(Comment category)
		{
			var descendants = new List<CommentDTO>();
			if (category.subComments != null)
			{
				foreach (var childCategory in category.subComments)
				{
					descendants.Add(new CommentDTO {Id= childCategory.Id,ReviewId=childCategory.ReviewId,Description=childCategory.Description,createdDate=childCategory.createdDate,ParentCommentId=childCategory.ParentCommentId });
					descendants.AddRange(GetAllDescendants(childCategory));
				}
			}
			return descendants;
		}
		public CommentDTO ProcessComment(Comment category)
		{
			
			// Retrieve and process descendants
			var descendants = GetAllDescendants(category);

			return new CommentDTO { FullName=category.User.FirstName+category.User.LastName,Image=category.User.Image,Id = category.Id, Description = category.Description, createdDate = category.createdDate, ReviewId = category.ReviewId, ParentCommentId = category.ParentCommentId, CommentDTO2 = descendants };
			
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
															.ThenInclude(c=>c.User)
															.Include(b=>b.booking)
															.Include(b=>b.booking.patient)
															.Include(b=>b.booking.doctor)
															.Include(b=>b.booking.doctor.doctor)
															.Where(b=>b.booking.doctor.doctor.Id==DoctorId)
															.Select(e=>new ReviewDTO { Image=e.booking.patient.Image,FullName=e.booking.patient.FirstName+ e.booking.patient.LastName, Id=e.Id,Title=e.Title,Description=e.Description,BookingId=e.BookingId,stars=e.stars,DateDays=(DateTime.Now-e.createdDate).Days,comments=e.Comments.ToList()})
															.ToListAsync();
			foreach(var review in list)
			{
				if (review.comments!=null)
				{foreach(var parentComment in review.comments.Where(E=>E.ParentCommentId==null))
				{
					var temp = ProcessComment(parentComment);
					review.CommentDTO.Add(temp);
				}

				}
				
				review.comments = null;
			}
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
