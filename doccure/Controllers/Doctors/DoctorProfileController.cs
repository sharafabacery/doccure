﻿using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles ="doctor")]
	public class DoctorProfileController : Controller
	{
		private readonly IUserProfileSettingsService userProfileSettingsService;
		private readonly ISpecalityService specalityService;

		public DoctorProfileController(IUserProfileSettingsService userProfileSettingsService,ISpecalityService specalityService) {
			this.userProfileSettingsService = userProfileSettingsService;
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
		public IActionResult DoctorProfile(DoctorProfileRequest doctorProfileRequest)
		{

			return RedirectToAction("DoctorProfile", "DoctorProfile");
		}
	}
}