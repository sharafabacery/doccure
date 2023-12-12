using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles = "doctor")]
	[Route("/Doctor/ScheduleTiming")]
	public class ScheduleTimingController : Controller
	{
		private readonly IScheduleTimingService scheduleTimingService;

		public ScheduleTimingController(IScheduleTimingService scheduleTimingService)
		{
			this.scheduleTimingService = scheduleTimingService;
		}
		public async Task< IActionResult> ScheduleTiming()
		{
			var user =await scheduleTimingService.GetTimingSlotofDoctor(User);
			if(user == null)
			{
				return RedirectToAction("DoctorProfile", "DoctorProfile");
			}
			else
			{
				ViewBag.User = user;
				return View();
			}
			
		}
	}
}
