using doccure.Data.Models;
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
		public async Task<IActionResult> Checkout(int BookingId)
		{
			var Booking = await BookingService.GetBookingById(BookingId, User);
			if (Booking == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Booking = Booking;
			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult>Checkout(CheckoutRequest checkoutRequest)
		{
			var Booking = await BookingService.Checkout(checkoutRequest, User);
			if (Booking == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Booking = Booking;
			}
			return View("BookingSuccess");
		}
		public async Task<IActionResult> RegisterBooking(RegisterBookingRequest registerBooking)
		{
			var Booking = await BookingService.RegisterBooking(registerBooking, User);
			if (Booking != null)
			{
				return RedirectToAction("Checkout", "Booking", new {BookingId=Booking.Id});
			}
			else
			{
				TempData["message"] = "Booking failed to Add scuessfully";
				return RedirectToAction("Index", "Booking");
			}
		}
	}
}
