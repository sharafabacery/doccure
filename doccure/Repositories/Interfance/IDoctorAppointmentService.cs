using doccure.Data.Models;
using doccure.Data.RequestModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IDoctorAppointmentService
	{
		public Task<List<Booking>> GetDoctorAppointmentWithinWeek(ClaimsPrincipal claims);
		public Task<Booking>UpdateAppiontmentStatus(ClaimsPrincipal claims, UpdateBookingStatusRequest updateBookingStatusRequest);
		public Task<Booking> GetAppiontmentById(ClaimsPrincipal claims, int BookingId);
	}
}
