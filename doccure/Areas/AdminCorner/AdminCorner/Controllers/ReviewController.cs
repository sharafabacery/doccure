using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	[Authorize(Roles = "admin")]
	public class ReviewController : Controller
	{
		private readonly IReviewListService reviewListService;

		public ReviewController(IReviewListService reviewListService) {
			this.reviewListService = reviewListService;
		}
		public async Task<IActionResult> Index()
		{
			var reviews=await reviewListService.GetAllReviews();
			ViewBag.reviews=reviews;
			return View();
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteReview(int id)
		{
			var reviews = await reviewListService.DeleteReview(id);
			if (reviews)
			{
				return Ok();
			}
			else
			{
				return NotFound();
			}
		}
	}
}
