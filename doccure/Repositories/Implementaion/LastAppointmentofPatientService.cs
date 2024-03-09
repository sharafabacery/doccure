using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class LastAppointmentofPatientService : ILastAppointmentofPatient
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public LastAppointmentofPatientService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}
		public async Task<Booking> LastAppoitment(ClaimsPrincipal claims, string patientId)
		
		{
			var Book = await applicationDbContext.Bookings
												.Include(p => p.Prescription)
												.Include(u=>u.patient)
												.Include(u=>u.patient.address)
												.Include(u=>u.doctor)
												.Include(u=>u.doctor.doctor.Speciality)
												.Include(u=>u.doctor.address)
												.Where(e => e.Prescription.Count == 0 && e.doctorId == userManager.GetUserId(claims) && e.patientId == patientId)
												.OrderByDescending(c => c.BookingStatus)
												.OrderByDescending(c => c.bookingDate)
												.FirstOrDefaultAsync();
			return Book;
		}
	}
}
