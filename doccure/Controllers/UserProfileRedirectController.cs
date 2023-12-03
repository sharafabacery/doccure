using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers
{
	[Authorize]
	public class UserProfileRedirectController : Controller
	{
		public IActionResult Index()
		{
			var user = User;
			if (user.IsInRole("patient"))
			{
				return RedirectToAction("UserProfile", "UserProfile");
			}else if (user.IsInRole("doctor"))
			{
				return RedirectToAction("DoctorProfile", "DoctorProfile");
			}
			return View();
		}
	}
}
