using doccure.Data.Models;
using doccure.Data;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class DoctorAppointmentService : IDoctorAppointmentService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public DoctorAppointmentService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager) {
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}
		public async Task<List<Booking>> GetDoctorAppointmentWithinWeek(ClaimsPrincipal claims)
		{
			var DateOfToday = DateTime.Now.Date;
			var DateOfTheNextWeekOfDay = DateTime.Now.AddDays(7).Date;

			var DoctorBooking =await applicationDbContext.Bookings.Include(p => p.patient).Include(A => A.patient.address).Include(s => s.scheduleTiming).Where(b => b.doctorId == userManager.GetUserId(claims) && b.bookingDate >= DateOfToday && b.bookingDate <= DateOfTheNextWeekOfDay).OrderByDescending(d=>d.bookingDate).ToListAsync();
			return DoctorBooking;
		}
	}
}
