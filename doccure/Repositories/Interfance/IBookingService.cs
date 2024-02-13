using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IBookingService
	{
		/*
		 SELECT *
		 FROM [dbo].[ScheduleTiming] S
	     WHERE S.Id NOT IN(
		 SELECT B.scheduletimingId
		 FROM [dbo].[Bookings] B
		 WHERE B.doctorId='' AND B.bookingDate IN ('','') 
		 ) AND S.ClinicId=4
		 ORDER BY S.Day,S.StartTime
		 */
		public Task<List<ScheduleTimingBooking>> GetScheduleTimingAvaiable(BookingScheduleTimingRequest booking);
		public Task<Booking> RegisterBooking(RegisterBookingRequest registerBookingRequest, ClaimsPrincipal claims);
		public Task<Booking> GetBookingById(int BookingId, ClaimsPrincipal claims);
		public Task<Booking> Checkout(CheckoutRequest checkoutRequest, ClaimsPrincipal claims);
	}
}
