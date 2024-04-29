using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static doccure.Program;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	[Authorize(Roles = "admin")]
	public class ProfileController : Controller
	{
		private readonly IUserProfileSettingsService userProfileSettingsService;
		public ProfileController(ServiceResolver serviceAccessor) {
			this.userProfileSettingsService = serviceAccessor("admin");
		}
		public async Task<IActionResult> Index()
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
		public async Task<IActionResult> Index(AdminProfileRequest AuserProfileRequest)
		{
			var result = await userProfileSettingsService.UpdateUserData(AuserProfileRequest.UserProfileRequest, User);
			if (result == null)
			{
				ViewBag.message = "Cant update the data";
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Profile");
			}
		}
		[HttpPost]

		public async Task<IActionResult> AdminPasswordUpdated(AdminProfileRequest _updatePasswordRequset)
		{
			var result = await userProfileSettingsService.UpdatePassword(_updatePasswordRequset.UpdatePasswordRequset, User);
			if (result == null)
			{
				ViewBag.message = "Cant update the password";
				return RedirectToAction("Index", "Profile");
			}
			else
			{
				ViewBag.User = result;
				return RedirectToAction("Index", "Profile");
			}
		}
	}
}
