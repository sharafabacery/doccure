using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.patient
{
	[Authorize]
	public class UserProfileController : Controller
	{
		private readonly IUserProfileSettingsService userProfileSettingsService;

		public UserProfileController(IUserProfileSettingsService userProfileSettingsService)
		{
			this.userProfileSettingsService = userProfileSettingsService;
		}
		[HttpGet]
		public async Task< IActionResult> UserProfile()
		{
			var user = await userProfileSettingsService.GetUserData(User);
			if(user == null)
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
		public async Task<IActionResult> UserProfile(UserProfileRequest userProfileRequest)
		{
			var result = await userProfileSettingsService.UpdateUserData(userProfileRequest, User);
			if(result == null)
			{
				ViewBag.message = "Cant update the data";
				return View();
			}
			else
			{
				return RedirectToAction("UserProfile", "UserProfile");
			}
		}

		[HttpGet]
		public async Task<IActionResult> UserPasswordUpdated()
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

		public async Task<IActionResult> UserPasswordUpdated(UpdatePasswordRequset updatePasswordRequset)
		{
			var result = await userProfileSettingsService.UpdatePassword(updatePasswordRequset,User);
			if (result == null)
			{
				ViewBag.message = "Cant update the password";
				return View();
			}
			else
			{
				ViewBag.User = result;
				return RedirectToAction("UserPasswordUpdated", "UserProfile");
			}
		}
	}
}
