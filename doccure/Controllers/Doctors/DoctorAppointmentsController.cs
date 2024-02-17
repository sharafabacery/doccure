using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;
using static doccure.Program;

namespace doccure.Controllers.Doctors
{
	public class DoctorAppointmentsController : Controller
	{
		private readonly IUserProfileSettingsService userProfileSettingsService;
		private readonly IDoctorAppointmentService doctorAppointmentService;

		public DoctorAppointmentsController(IDoctorAppointmentService doctorAppointmentService, ServiceResolver serviceAccessor)
		{
			this.userProfileSettingsService = serviceAccessor("Doctor");
			this.doctorAppointmentService = doctorAppointmentService;
		}
		public async Task<IActionResult> Index()
		{
			var Doctor = await userProfileSettingsService.GetUserData(User);
			var Appointments = await doctorAppointmentService.GetDoctorAppointmentWithinWeek(User);
			ViewBag.Appointments = Appointments;
			ViewBag.User = Doctor;
			return View();
		}
		//BookingId
		[HttpPut]
		public async Task<IActionResult> UpdateAppointmentStatus(int BookingId, [FromBody] UpdateBookingStatusRequest updateBookingStatusRequest)
		{
			var Booking = await doctorAppointmentService.UpdateAppiontmentStatus(User,updateBookingStatusRequest);
			if (Booking != null)
			{
				return Ok();
			}
			else
			{
				return NotFound();
			}
		}
		[HttpGet]
		public async Task<IActionResult>GetAppiontmentById(int BookingId)
		{

			var Booking = await doctorAppointmentService.GetAppiontmentById(User, BookingId);
			if (Booking != null)
			{
				return Ok(Booking);
			}
			else
			{
				return NotFound();
			}
		}
	}
}
