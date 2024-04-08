using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class PatientAppiontmentProfile : IPatientAppiontmentProfile
	{
		
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public PatientAppiontmentProfile(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}
		public async Task<List<Booking>> GetAllAppoitementPatient(ClaimsPrincipal claims)
		{
			var Books = await applicationDbContext.Bookings.Include(a => a.doctor)
														   .Include(a => a.doctor.doctor)
															.Include(a => a.doctor.doctor.Speciality)
														  .Include(p => p.Prescription)
														  .Include(m => m.MedicalRecord)
														  .Include(p => p.Billing)
														  .Where(e => e.patientId == userManager.GetUserId(claims))
														  .OrderByDescending(c => c.BookingStatus)
														  .OrderByDescending(c => c.bookingDate)
														  .ToListAsync();
			return Books;
		}
	}
}
