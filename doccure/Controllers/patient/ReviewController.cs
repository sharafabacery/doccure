using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.patient
{
	public class ReviewController : Controller
	{
		private readonly IReviewService reviewService;
		private readonly IDoctorSearch doctorSearch;

		public ReviewController(IReviewService reviewService, IDoctorSearch doctorSearch)
		{
			this.reviewService = reviewService;
			this.doctorSearch = doctorSearch;
		}
		public async Task<IActionResult> AddReview(ReviewRequest reviewRequest)
		{
			var review = await reviewService.AddReview(User,reviewRequest);
			
			if(review != null)
			{
				return RedirectToActionPermanentPreserveMethod(nameof(DoctorSearchController.GetDoctor), "DoctorSearch",new {Id=review.booking.doctorId});
			}
			else
			{
				var doctor = await doctorSearch.GetDoctorDataByDoctorId(reviewRequest.DoctorID);
				return RedirectToActionPermanentPreserveMethod(nameof(DoctorSearchController.GetDoctor), "DoctorSearch", new { Id = doctor.Id });

			}
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
