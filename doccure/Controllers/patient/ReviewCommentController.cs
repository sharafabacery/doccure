using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.patient
{
	public class ReviewCommentController : Controller
	{
		private readonly IReviewCommentService reviewService;
		private readonly IDoctorSearch doctorSearch;


		public ReviewCommentController(IReviewCommentService reviewService, IDoctorSearch doctorSearch)
		{
			this.reviewService = reviewService;
			this.doctorSearch = doctorSearch;

		}



		public async Task<IActionResult> AddComment2Review(ReviewRequest comment)
		{
			var commentdb = await reviewService.AddComment2Review(User, comment.comment);
			if (commentdb!=null)
			{
				return RedirectToActionPermanentPreserveMethod(nameof(DoctorSearchController.GetDoctor), "DoctorSearch", new { Id = commentdb.booking.doctorId });
			}
			else
			{
				var doctor = await doctorSearch.GetDoctorDataByDoctorId(comment.DoctorID);
				return RedirectToActionPermanentPreserveMethod(nameof(DoctorSearchController.GetDoctor), "DoctorSearch", new { Id = doctor.Id });

			}
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
