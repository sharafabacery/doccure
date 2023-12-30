using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static doccure.Program;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles ="doctor")]
	public class DoctorProfileController : Controller
	{
		private readonly IUserProfileSettingsService userProfileSettingsService;
		private readonly ISpecalityService specalityService;

		public DoctorProfileController(ServiceResolver serviceAccessor, ISpecalityService specalityService) {
			this.userProfileSettingsService = serviceAccessor("Doctor");
			this.specalityService = specalityService;
		}
		[HttpGet]
		public async Task<IActionResult> DoctorProfile()
		{
			var user = await userProfileSettingsService.GetUserData(User);
			var specialities=await specalityService.GetSpecialities();
			if (user == null)
			{
				return NotFound();
			}
			else
			{
				ViewBag.User = user;
				ViewBag.Specialities = specialities;
			}
			return View();
		}

		[HttpPost]
		public async Task< IActionResult> DoctorProfile(DoctorProfileRequest doctorProfileRequest)
		{
			var doctor=await userProfileSettingsService.UpdateUserData(doctorProfileRequest, User);
			if(doctor == null)
			{
				return RedirectToAction("DoctorProfile", "DoctorProfile");
			}
			else
			{
				var specialities = await specalityService.GetSpecialities();
				ViewBag.User = doctor;
				ViewBag.Specialities = specialities;

			}
			return View();
		}
		public async Task<IActionResult> DoctorPasswordUpdated()
		{
			var user = await userProfileSettingsService.GetUserData(User);
			if (user == null)
			{
				return NotFound();
			}
			else
			{
				ViewBag.User = user;
			}
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> DoctorPasswordUpdated(UpdatePasswordRequset updatePasswordRequset)
		{
			var result = await userProfileSettingsService.UpdatePassword(updatePasswordRequset, User);
			if (result == null)
			{
				TempData["message"] = "Cant update the password";

				return RedirectToAction("DoctorPasswordUpdated", "DoctorProfile");
			}
			else
			{
				ViewBag.User = result;
				return RedirectToAction("DoctorPasswordUpdated", "DoctorProfile");
			}
		}
	}
}
