using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	public class PatientAppiontmentProfileDoctorViewController : Controller
	{
		private readonly IPatientAppiontmentProfileDoctorSide patientAppiontmentProfileDoctorSide;

		public PatientAppiontmentProfileDoctorViewController(IPatientAppiontmentProfileDoctorSide patientAppiontmentProfileDoctorSide)
		{
			this.patientAppiontmentProfileDoctorSide = patientAppiontmentProfileDoctorSide;
		}
		public async Task<IActionResult> Index(string Id)
		{
			var Patient=await patientAppiontmentProfileDoctorSide.GetPatientProfileData(Id);
			var Books = await patientAppiontmentProfileDoctorSide.GetAllAppoitementPatientByDoctorId(User, Id);

			ViewBag.Books = Books;
			ViewBag.Patient= Patient;
			return View("../PatientAppiontmentProfile/Index");
		}
	}
}
