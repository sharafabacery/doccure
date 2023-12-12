using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	public class ScheduleTimingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
