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
		private readonly IDoctorClinicService doctorClinicService;

		public ScheduleTimingController(IScheduleTimingService scheduleTimingService,IDoctorClinicService doctorClinicService)
		{
			this.scheduleTimingService = scheduleTimingService;
			this.doctorClinicService = doctorClinicService;
		}
		public async Task<IActionResult> ScheduleTiming()
		{
			var user =await doctorClinicService.GetDoctorClinics(User);
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
		[HttpGet]
		public async Task<IActionResult> GetSlotsofClinic(int ClinicId)
		{
			var UserSlots=await scheduleTimingService.GetTimingSlotByClinicId(ClinicId,User);
			if (UserSlots == null || UserSlots.doctor?.clinics?.FirstOrDefault()?.scheduleTiming.Count==0)
			{
				return NotFound();
			}
			else
			{
				return Ok(UserSlots);
			}
		}
	}
}
