using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static doccure.Program;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles = "doctor")]
	public class PatientsController : Controller
	{
		private readonly IPatientsService PatientsService;
		private readonly IUserProfileSettingsService userProfileSettingsService;

		public PatientsController(IPatientsService PatientsService, ServiceResolver serviceAccessor)
		{
			this.PatientsService = PatientsService;
			this.userProfileSettingsService = serviceAccessor("Doctor");
		}
		public async Task<IActionResult> MyPatients()
		{
			var Doctor =await userProfileSettingsService.GetUserData(User);
			var Patients =await PatientsService.GetPatientsByDoctorId(User);
			ViewBag.Patients = Patients;
			ViewBag.User = Doctor;
			return View();
		}
	}
}
