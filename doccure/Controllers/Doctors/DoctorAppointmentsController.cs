using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;
using static doccure.Program;

namespace doccure.Controllers.Doctors
{
	public class DoctorAppointmentsController : Controller
	{
		private readonly IUserProfileSettingsService userProfileSettingsService;
		private readonly IDoctorAppointmentService doctorAppointmentService;

		public DoctorAppointmentsController(IDoctorAppointmentService doctorAppointmentService, ServiceResolver serviceAccessor)
		{
			this.userProfileSettingsService = serviceAccessor("Doctor");
			this.doctorAppointmentService = doctorAppointmentService;
		}
		public async Task<IActionResult> Index()
		{
			var Doctor = await userProfileSettingsService.GetUserData(User);
			var Appointments = await doctorAppointmentService.GetDoctorAppointmentWithinWeek(User);
			ViewBag.Appointments = Appointments;
			ViewBag.User = Doctor;
			return View();
		}
	}
}
