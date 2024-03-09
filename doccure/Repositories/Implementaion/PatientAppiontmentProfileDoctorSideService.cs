using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class PatientAppiontmentProfileDoctorSideService : IPatientAppiontmentProfileDoctorSide
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public PatientAppiontmentProfileDoctorSideService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}
		public async Task<List<Booking>> GetAllAppoitementPatientByDoctorId(ClaimsPrincipal claims, string patientId)
		{
			var Books = await applicationDbContext.Bookings.Include(a => a.doctor)
														   .Include(a => a.doctor.doctor)
															.Include(a=>a.doctor.doctor.Speciality)
														  .Include(p => p.Prescription)
														  .Include(m => m.MedicalRecord)
														  .Include(p => p.Billing)
														  .Where(e => e.doctorId == userManager.GetUserId(claims) &&e.patientId==patientId)
														  .OrderByDescending(c => c.BookingStatus)
														  .OrderByDescending(c => c.bookingDate)
														  .ToListAsync();
			return Books;
		}

		public async Task<Applicationuser> GetPatientProfileData(string Id)
		{
			var user=await applicationDbContext.Users.Include(a=>a.address).FirstOrDefaultAsync(u => u.Id == Id);

			return user;
		}
	}
}
