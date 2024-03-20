using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class LastMedicalRecordBookingPatientService : ILastMedicalRecordBookingPatientService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public LastMedicalRecordBookingPatientService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}
		public async Task<Booking> LastMedicalRecord(ClaimsPrincipal claims, string patientId)
		{
			var Book = await applicationDbContext.Bookings
												.Include(m=>m.MedicalRecord)
												.Where(e=>e.MedicalRecord==null&&e.patientId==patientId)
												.OrderByDescending(c => c.BookingStatus)
												.OrderByDescending(c => c.bookingDate)
												.FirstOrDefaultAsync();
			return Book;
		}
	}
}
