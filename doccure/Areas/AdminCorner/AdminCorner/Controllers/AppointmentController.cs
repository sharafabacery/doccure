using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	[Authorize(Roles = "admin")]
	public class AppointmentController : Controller
	{
		private readonly IAppointmentListService appointmentListService;

		public AppointmentController(IAppointmentListService appointmentListService)
		{
			this.appointmentListService = appointmentListService;
		}
		public async Task< IActionResult> Index()
		{
			var Appointments=await appointmentListService.GetAllAppointments();
			ViewBag.Appointments = Appointments;
			return View();
		}
	}
}
