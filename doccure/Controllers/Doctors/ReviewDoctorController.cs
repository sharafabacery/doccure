using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static doccure.Program;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles ="doctor")]
	public class ReviewDoctorController : Controller
	{
		private readonly IReviewService reviewService;
		private readonly IUserProfileSettingsService userProfileSettingsService;

		public ReviewDoctorController(IReviewService reviewService, ServiceResolver serviceAccessor)
		{
			this.reviewService = reviewService;
			this.userProfileSettingsService = serviceAccessor("Doctor");
		}
		public async Task<IActionResult> Index()
		{
			var user = await userProfileSettingsService.GetUserData(User);
			var reviews = await reviewService.GetAllReviews(user.doctor.Id);
			ViewBag.User= user;
			ViewBag.Reviews=reviews;
			return View();
		}
	}
}
