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
			var DateOfToday = DateTime.Now.ToString();
			var DateOfTheNextWeekOfDay= DateTime.Now.AddDays(7).ToString();

			List<ScheduleTimingBooking> AvaiableBooking=new List<ScheduleTimingBooking>();
			if(doctor != null) {
				string ss = $"SELECT *,CONVERT(varchar,DATEADD(DAY,S.Day,'{DateOfToday}'),1) BookingDate,DATENAME(dw,DATEADD(DAY,S.Day,'{DateOfToday}')) BookingDateDay \r\nFROM [dbo].[ScheduleTiming] S\r\nWHERE S.Id NOT IN(\r\nSELECT B.scheduletimingId\r\nFROM [dbo].[Bookings] B\r\nWHERE B.doctorId='{doctor.applicationuserId}' AND B.bookingDate IN ('{DateOfToday}','{DateOfTheNextWeekOfDay}') \r\n) AND S.ClinicId={booking.ClinicID} \r\nORDER BY BookingDate, S.Day,S.StartTime";
				AvaiableBooking=await applicationDbContext.ScheduleTimingBooking.FromSqlRaw($"SELECT S.Id,S.Day,CONVERT(varchar,S.StartTime) StartTime,CONVERT(varchar,S.EndTime) EndTime,CONVERT(varchar,DATEADD(DAY,S.Day,'{DateOfToday}'),1) BookingDate,DATENAME(dw,DATEADD(DAY,S.Day,'{DateOfToday}')) BookingDateDay \r\nFROM [dbo].[ScheduleTiming] S\r\nWHERE S.Id NOT IN(\r\nSELECT B.scheduletimingId\r\nFROM [dbo].[Bookings] B\r\nWHERE B.doctorId='{doctor.applicationuserId}' AND B.bookingDate IN ('{DateOfToday}','{DateOfTheNextWeekOfDay}') \r\n) AND S.ClinicId={booking.ClinicID} \r\nORDER BY BookingDate, S.Day,S.StartTime").ToListAsync();
			}
			
			
				return AvaiableBooking;
			
			
		}
	}
}
