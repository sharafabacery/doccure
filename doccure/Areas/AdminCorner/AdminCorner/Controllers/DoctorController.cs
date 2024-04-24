using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	public class DoctorController : Controller
	{
		private readonly IDoctorListDashboardService userDashboardService;

		public DoctorController(IDoctorListDashboardService userDashboardService)
		{
			this.userDashboardService = userDashboardService;
		}
		public async Task< IActionResult> Index()
		{
			var users =await userDashboardService.GetAllUsers();
			ViewBag.users = users;
			return View();
		}
	}
}
