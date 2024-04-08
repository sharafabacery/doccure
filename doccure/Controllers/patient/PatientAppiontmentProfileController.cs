using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static doccure.Program;
using static System.Reflection.Metadata.BlobBuilder;

namespace doccure.Controllers.patient
{
    public class PatientAppiontmentProfileController : Controller
    {
		private readonly IUserProfileSettingsService userProfileSettingsService;
		private readonly IPatientAppiontmentProfile patientAppiontmentProfile;

		public PatientAppiontmentProfileController(IPatientAppiontmentProfile patientAppiontmentProfile, ServiceResolver serviceAccessor)
        {
			this.patientAppiontmentProfile = patientAppiontmentProfile;
			this.userProfileSettingsService = serviceAccessor("Patient");
		}
		[Authorize]
		[HttpGet]
		public async Task< IActionResult> Index()
        {
			var user = await userProfileSettingsService.GetUserData(User);
			var books = await patientAppiontmentProfile.GetAllAppoitementPatient(User);
			if (user == null||books==null)
			{
				return NotFound();
			}
			ViewBag.Books = books;
			ViewBag.User = user;
			ViewBag.Patient = user;
			return View("../PatientAppiontmentProfile/Index");
		}
    }
}
