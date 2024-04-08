using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	public class FavouritesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
