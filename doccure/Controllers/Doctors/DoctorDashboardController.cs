using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static doccure.Program;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles = "doctor")]
	public class DoctorDashboardController : Controller
	{
		private readonly IDoctorDashboardService doctorDashboardService;
		private readonly IUserProfileSettingsService userProfileSettingsService;

		public DoctorDashboardController(ServiceResolver serviceAccessor,IDoctorDashboardService doctorDashboardService) {
			this.doctorDashboardService = doctorDashboardService;
			this.userProfileSettingsService = serviceAccessor("Doctor");
		}
		public async Task<IActionResult> Index()
		{
			var result =await doctorDashboardService.DoctorDashboardData(User);
			var doctor=await userProfileSettingsService.GetUserData(User);
			ViewBag.Result = result;
			ViewBag.User = doctor;
			return View();
		}
	}
}
