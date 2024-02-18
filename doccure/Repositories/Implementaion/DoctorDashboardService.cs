using doccure.Data.Models;
using doccure.Data;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class DoctorDashboardService : IDoctorDashboardService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public DoctorDashboardService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}
		public async Task<DoctorDashboardReturned> DoctorDashboardData(ClaimsPrincipal claims)
		{
			var DateOfToday = DateTime.Now.Date;
			var DateOfTheNextWeekOfDay = DateTime.Now.AddDays(7).Date;

			FormattableString sql = $"SELECT Count(patientId) FROM [Bookings] WHERE doctorId = {userManager.GetUserId(claims)} ";
			FormattableString sql2 = $"SELECT Count(patientId) FROM [Bookings] WHERE doctorId = {userManager.GetUserId(claims)} AND bookingDate = {DateOfToday}";
			FormattableString sql3 = $"SELECT Count(*) FROM [Bookings] WHERE doctorId = {userManager.GetUserId(claims)}";
			var TotalPatients =await applicationDbContext.Database
									.SqlQuery<int>(sql).ToListAsync();
			var TotalTodayPatients = await applicationDbContext.Database.SqlQuery<int>(sql2).ToListAsync(); 
			var TotalAppiontment = await applicationDbContext.Database.SqlQuery<int>(sql3).ToListAsync();
			var Books = await applicationDbContext.Bookings.Include(p=>p.patient).Include(s=>s.scheduleTiming).Where(b => b.doctorId == userManager.GetUserId(claims) && b.bookingDate <= DateOfTheNextWeekOfDay && b.bookingDate >= DateOfToday).ToListAsync();
			var TodayBooks = new List<Booking>(Books);
			TodayBooks=TodayBooks.Where(b => b.bookingDate == DateOfToday).ToList();
			
			var doctorDashboardReturned=new DoctorDashboardReturned();
			doctorDashboardReturned.TotalPatients = TotalPatients[0];
			doctorDashboardReturned.TotalTodayPatients = TotalTodayPatients[0];
			doctorDashboardReturned.TotalAppiontment=TotalAppiontment[0];
			doctorDashboardReturned.Today = TodayBooks;
			doctorDashboardReturned.Upcoming = Books;
			return doctorDashboardReturned;

		}
	}
}
