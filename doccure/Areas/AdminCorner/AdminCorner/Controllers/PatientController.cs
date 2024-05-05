using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	[Authorize(Roles = "admin")]
	public class PatientController : Controller
	{
		private readonly IPatientListService patientListService;

		public PatientController(IPatientListService patientListService)
		{
			this.patientListService = patientListService;
		}
		public async Task<IActionResult> Index()
		{
			bool pagination = false;
			var users = await patientListService.GetAllUsers(pagination);
			ViewBag.users = users;
			return View();
		}
	}
}
