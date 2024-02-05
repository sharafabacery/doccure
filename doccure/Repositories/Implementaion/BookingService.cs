using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class BookingService : IBookingService
	{
		private readonly ApplicationDbContext applicationDbContext;

		public BookingService( ApplicationDbContext applicationDbContext) {
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<List<ScheduleTimingBooking>> GetScheduleTimingAvaiable(BookingScheduleTimingRequest booking)
		{
			var doctor = applicationDbContext.Doctor.FirstOrDefault(i=>i.Id==booking.DoctorId);
			var DateOfToday = DateTime.Now;
			var DateOfTheNextWeekOfDay= DateOfToday.AddDays(7);

			List<ScheduleTimingBooking> AvaiableBooking=new List<ScheduleTimingBooking>();
			if(doctor != null) {
				AvaiableBooking =await applicationDbContext.ScheduleTimingBooking.FromSqlInterpolated($"SELECT *,CONVERT(varchar,DATEADD(DAY,S.Day,'{DateOfToday.ToString()}')) BookingDate,DATENAME(dw,DATEADD(DAY,S.Day,'{DateOfToday.ToString()}')) BookingDateDay \r\nFROM [dbo].[ScheduleTiming] S\r\nWHERE S.Id NOT IN(\r\nSELECT B.scheduletimingId\r\nFROM [dbo].[Bookings] B\r\nWHERE B.doctorId='{doctor.applicationuserId}' AND B.bookingDate IN ('{DateOfToday.ToString()}','{DateOfTheNextWeekOfDay.ToString()}') \r\n) AND S.ClinicId={booking.ClinicID} \r\nORDER BY BookingDate, S.Day,S.StartTime").ToListAsync();
			}
			if (!AvaiableBooking.Any())
			{
				return null;
			}
			else
			{
				return AvaiableBooking;
			}
			
		}
	}
}
