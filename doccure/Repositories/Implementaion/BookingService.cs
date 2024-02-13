using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Transactions;

namespace doccure.Repositories.Implementaion
{
	public class BookingService : IBookingService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public BookingService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}
		public async Task<List<ScheduleTimingBooking>> GetScheduleTimingAvaiable(BookingScheduleTimingRequest booking)
		{
			var doctor = applicationDbContext.Doctor.FirstOrDefault(i => i.Id == booking.DoctorId);
			var DateOfToday = DateTime.Now.Date.ToString();
			var DateOfTheNextWeekOfDay = DateTime.Now.AddDays(7).Date.ToString();

			List<ScheduleTimingBooking> AvaiableBooking = new List<ScheduleTimingBooking>();
			if (doctor != null)
			{
				string S = $"SELECT S.Id,S.Day,CONVERT(varchar,S.StartTime) StartTime,CONVERT(varchar,S.EndTime) EndTime,CONVERT(varchar,DATEADD(DAY,S.Day,{DateOfToday}),1) BookingDate,DATENAME(dw,DATEADD(DAY,S.Day,{DateOfToday})) BookingDateDay \r\nFROM [dbo].[ScheduleTiming] S\r\nWHERE S.Id NOT IN(\r\nSELECT B.scheduletimingId\r\nFROM [dbo].[Bookings] B\r\nWHERE B.doctorId='{doctor.applicationuserId}'  AND B.bookingDate >= '{DateOfToday}' AND B.bookingDate <= '{DateOfTheNextWeekOfDay}' \r\n) AND S.ClinicId={booking.ClinicID} \r\nORDER BY BookingDate, S.Day,S.StartTime";
				AvaiableBooking = await applicationDbContext.ScheduleTimingBooking.FromSqlRaw($"SELECT S.Id,S.Day,CONVERT(varchar,S.StartTime) StartTime,CONVERT(varchar,S.EndTime) EndTime,CONVERT(varchar,DATEADD(DAY,S.Day,'{DateOfToday}'),1) BookingDate,DATENAME(dw,DATEADD(DAY,S.Day,'{DateOfToday}')) BookingDateDay \r\nFROM [dbo].[ScheduleTiming] S\r\nWHERE S.Id NOT IN(\r\nSELECT B.scheduletimingId\r\nFROM [dbo].[Bookings] B\r\nWHERE B.doctorId='{doctor.applicationuserId}'  AND B.bookingDate >= '{DateOfToday}' AND B.bookingDate <= '{DateOfTheNextWeekOfDay}' \r\n) AND S.ClinicId={booking.ClinicID} \r\nORDER BY BookingDate, S.Day,S.StartTime").ToListAsync();
			}


			return AvaiableBooking;


		}

		public async Task<Booking> RegisterBooking(RegisterBookingRequest registerBookingRequest, ClaimsPrincipal claims)
		{
			Booking booking;

			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					var Slot=await applicationDbContext.ScheduleTiming.FromSql($"SELECT * FROM ScheduleTiming WITH (UPDLOCK) WHERE Id={registerBookingRequest.Id}").FirstOrDefaultAsync();
					var Booked=await applicationDbContext.Bookings.Where(b=>b.scheduletimingId==Slot.Id && b.bookingDate.Date ==registerBookingRequest.BookingDate.Date).FirstOrDefaultAsync();
					var Doctor = await applicationDbContext.Doctor.FirstOrDefaultAsync(d => d.Id == registerBookingRequest.DoctorId);
					if (Booked != null)
					{
						throw new Exception("schedule timing in that time was booked");
					}
					else
					{
						booking = new Booking();
						booking.patientId = userManager.GetUserId(claims);
						booking.doctorId = Doctor.applicationuserId;
						booking.price = (double)Doctor.Price;
						booking.videocall = (double)Doctor.VideoCall;
						booking.total = (double)Doctor.Price + (double)Doctor.VideoCall;
						booking.bookingDate =registerBookingRequest.BookingDate;
						booking.payment = PaymentMethod.NotPayed;
						booking.scheduletimingId = Slot.Id;
						 applicationDbContext.Bookings.Add(booking);
						var Inserted = await applicationDbContext.SaveChangesAsync();
						if (Inserted > 0)
						{

							scope.Complete();
						}

					}
				}
				catch (Exception ex)
				{
					booking = null;
					scope.Dispose();
				}
			}


			return booking;
		}

		public async Task<Booking> GetBookingById(int BookingId, ClaimsPrincipal claims)
		{
			var Booking = await applicationDbContext.Bookings.Include(p=>p.patient).Include(d=>d.doctor).Include(s=>s.scheduleTiming).Where(b => b.Id == BookingId && b.patientId == userManager.GetUserId(claims)).FirstOrDefaultAsync();
			if(Booking == null)
			{
				return null;
			}
			else
			{
				return Booking;
			}
		}

		public async Task<Booking> Checkout(CheckoutRequest checkoutRequest, ClaimsPrincipal claims)
		{
			var Booking = await applicationDbContext.Bookings.Include(p => p.patient).Include(d => d.doctor).Include(s => s.scheduleTiming).Where(b => b.Id == checkoutRequest.BookingId && b.patientId == userManager.GetUserId(claims)).FirstOrDefaultAsync();
			if(Booking == null)
			{
				return null;
			}
			else
			{
				if (checkoutRequest.CreditCard == true) { 
					Booking.payment=PaymentMethod.CreditCard;
				}else if (checkoutRequest.Paypal == true)
				{
					Booking.payment = PaymentMethod.Paypal;
				}

				var result=await applicationDbContext.SaveChangesAsync();
				if (result == 0)
				{
					return null;
				}

			}
			return Booking;
			
		}
	}
}
