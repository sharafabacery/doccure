using doccure.Data.Models;
using doccure.Data;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using doccure.Data.RequestModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class PrescriptionService: IPrescriptionService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public PrescriptionService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}

		public async Task<Booking> AddEditPrescription(PrescriptionRequest prescriptionRequest)
		{
			var book = await applicationDbContext.Bookings.Include(p => p.Prescription).Where(p => p.Id == prescriptionRequest.BookingId).FirstOrDefaultAsync();
			if (book == null) { return null; }
			else
			{
				foreach(var pp in prescriptionRequest.prescriptions)
				{
					var Prescriptiondb=book.Prescription.FirstOrDefault(p=>p.Id == pp.Id);
					if(Prescriptiondb == null)
					{
						book.Prescription.Add(pp);
					}
					else
					{
						Prescriptiondb.Name = pp.Name;

                        Prescriptiondb.Days=pp.Days;
						Prescriptiondb.Quantity=pp.Quantity;
						Prescriptiondb.Afternoon=pp.Afternoon;
						Prescriptiondb.Morning=pp.Morning;
						Prescriptiondb.Evening=pp.Evening;
						Prescriptiondb.Night=pp.Night;

					}
				}
				var res=await applicationDbContext.SaveChangesAsync();
				if (res > 0)
				{
					return book;
				}
				else
				{
					return null;
				}
			}
			
		}

		public async Task<bool> DeleteAllPrescriptionByBookingId(int BookingId)
		{
			var Prescriptiondb = await applicationDbContext.Prescriptions.Where(p => p.BookingId == BookingId).ExecuteDeleteAsync();
			if (Prescriptiondb > 0) return true;
			else return false;
		}

		public async Task<bool> DeletePrescription(int PrescriptionId, ClaimsPrincipal claims)
		{
			var Prescriptiondb= await applicationDbContext.Prescriptions.Include(b=>b.booking).FirstOrDefaultAsync(p=>p.Id==PrescriptionId);
			if(Prescriptiondb == null)
			{
				return false;
			}
			else
			{
				if(Prescriptiondb.booking.doctorId == userManager.GetUserId(claims))
				{
					applicationDbContext.Remove(Prescriptiondb);
					await applicationDbContext.SaveChangesAsync();
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public async Task<Booking> GetPrescriptionsByBookingId(int BookingId)
		{
			var BookPrescriptiondb = await applicationDbContext.Bookings
																.Include(p=>p.patient)
																.Include(p => p.patient.address)
																.Include(d=>d.doctor)
																.Include(d=>d.doctor.doctor.Speciality)
																.Include(d => d.doctor.address)
																.Include(p=>p.Prescription)
																.Where(b => b.Id == BookingId)
																.FirstOrDefaultAsync();
			if(BookPrescriptiondb == null)
			{
				return null;
			}
			else
			{
				return BookPrescriptiondb;
			}
		}
	}
}
