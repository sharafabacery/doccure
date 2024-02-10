using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using doccure.Repositories.Implementaion;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace doccure.Controllers.patient
{
	[Authorize]
	public class BookingController : Controller
	{
		private readonly IBookingService BookingService;
		private readonly IDoctorSearch doctorSearch;
		public BookingController(IBookingService bookingService,IDoctorSearch doctorSearch)
		{
			this.BookingService = bookingService;
			this.doctorSearch = doctorSearch;
		}
		public async Task <IActionResult> Index(BookingScheduleTimingRequest bookingScheduleTimingRequest)
		{
			var Doctor = await doctorSearch.GetDoctorDataByDoctorId(bookingScheduleTimingRequest.DoctorId);
			List<ScheduleTimingBooking> AvaiableBooking=new List<ScheduleTimingBooking>();
			if (Doctor != null)
			{
				AvaiableBooking =await BookingService.GetScheduleTimingAvaiable(bookingScheduleTimingRequest);
			}
			else
			{
				//404 page
			}
			
			
				ViewBag.Doctor = Doctor;
				ViewBag.AvaiableBooking = AvaiableBooking;
				return View();
		}
	}
}
