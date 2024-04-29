using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	[Authorize(Roles = "admin")]
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
			ViewBag.doctors = users;
			return View();
		}
	}
}
