using doccure.Controllers.patient;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles ="doctor")]
	public class ReviewCommentDoctorController : Controller
	{
		private readonly IReviewCommentService reviewService;

		public ReviewCommentDoctorController(IReviewCommentService reviewService) {
			this.reviewService = reviewService;
		}
		public async Task<IActionResult> AddComment2Review(ReviewCommentRequest comment)
		{
			var commentdb = await reviewService.AddComment2Review(User, new Comment() { Description = comment.Description, ReviewId = comment.ReviewId, ParentCommentId = comment.ParentCommentId == 0 ? null : comment.ParentCommentId });
			if (commentdb != null)
			{
				return RedirectToAction("Index", "ReviewDoctor");
			}
			else
			{
				TempData["comment"] = "cant saved";
				return RedirectToAction("Index", "ReviewDoctor");
			}
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
