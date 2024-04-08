using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	[Authorize]
	public class FavouritesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
