using doccure.Data.RequestModels;
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
		[Route("")]
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
		[Route("")]
		[HttpPost]
		public async Task<IActionResult> ScheduleTiming(ScheduleTimingSlotRequest scheduleTimingSlotRequest)
		{
			var result = await scheduleTimingService.AddTimingSlot(scheduleTimingSlotRequest, User);
			if (result == null)
			{
				return RedirectToAction("DoctorProfile", "DoctorProfile");
			}
			else
			{

				return RedirectToAction("ScheduleTiming", "ScheduleTiming");
			}
		}
		[Route("/Doctor/ScheduleTiming/ScheduleTimingEdited")]
		[HttpPost]
		public async Task<IActionResult> ScheduleTimingEdited(ScheduleTimingSlotRequest scheduleTimingSlotRequest)
		{
			var result = await scheduleTimingService.UpdateTimingSlot(scheduleTimingSlotRequest, User);
			if (result == null)
			{
				TempData["message"] = "can not edit time slots";
				return RedirectToAction("ScheduleTiming", "ScheduleTiming");
			}
			else
			{

				return RedirectToAction("ScheduleTiming", "ScheduleTiming");
			}
		}
		[Route("/Doctor/ScheduleTiming/GetSlotsofClinic/{ClinicId}")]
		public async Task<IActionResult> GetSlotsofClinic(int ClinicId)
		{
			var UserSlots=await scheduleTimingService.GetTimingSlotByClinicId(ClinicId,User);
			if (UserSlots == null ||UserSlots.Count==0)
			{
				return NotFound();
			}
			else
			{
				return Ok(UserSlots);
			}
		}
		[Route("/Doctor/ScheduleTiming/GetTimingSlotofDay/{ClinicId}/{day}")]
		public async Task<IActionResult> GetTimingSlotofDay(int ClinicId,int day) {
			var UserSlots=await scheduleTimingService.GetTimingSlotofDay(day,ClinicId,User);
			if (UserSlots == null || UserSlots.Count == 0)
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
