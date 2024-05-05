using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	[Authorize(Roles ="admin")]
	public class HomeController : Controller
	{
		private readonly IStaticticsService staticticsService;
		private readonly IDoctorListDashboardService doctorListDashboardService;
		private readonly IPatientListService patientListService;
		private readonly IAppointmentListService appointmentListService;

		public HomeController(IStaticticsService staticticsService, IDoctorListDashboardService doctorListDashboardService, IPatientListService patientListService, IAppointmentListService appointmentListService)
		{
			this.staticticsService = staticticsService;
			this.doctorListDashboardService = doctorListDashboardService;
			this.patientListService = patientListService;
			this.appointmentListService = appointmentListService;
		}
		public async Task<IActionResult> Index()
		{
			bool pagination = true;
			var doctors = await doctorListDashboardService.GetAllUsers(pagination);
			var users = await patientListService.GetAllUsers(pagination);
			var appointments = await appointmentListService.GetAllAppointments(pagination);
			var CountDoctors = await staticticsService.GetUserCountByRole("patient");
			var CountUsers = await staticticsService.GetUserCountByRole("patient");
			var countAppoinments = await staticticsService.GetAppoinmentCount();
			var RevnueTotal = await staticticsService.GetRevnueTotal();
			var Revnue = await staticticsService.Revenue();
			var userCounter = await staticticsService.GetUsersYears();
			ViewBag.doctors = doctors;
			ViewBag.users = users;
			ViewBag.appointments = appointments;
			ViewBag.CountDoctors = CountDoctors;
			ViewBag.CountUsers = CountUsers;
			ViewBag.countAppoinments = countAppoinments;
			ViewBag.RevnueTotal = RevnueTotal;
			ViewBag.Revnue = Revnue.ToJson();
			ViewBag.userCounter = userCounter.ToJson();
			return View();
		}
	}
}
