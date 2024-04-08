using doccure.Data;
using doccure.Data.Migrations;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class BillingService : IBillingService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public BillingService( ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
			
		}
		public async Task<Booking> AddEditBilling(BillingRequest billingRequest)
		{
			var book = await applicationDbContext.Bookings.Include(p => p.Billing).Where(p => p.Id == billingRequest.BookingId).FirstOrDefaultAsync();
			float total = 0;
			if (book == null) { return null; }
			else
			{
				var newBill = billingRequest.Bills.FindAll(p => p.Id == 0);
				var updateBill = billingRequest.Bills.FindAll(p => p.Id != 0);

				foreach (var pp in newBill)
				{

					book.Billing.Add(pp);
					total += pp.Amount;
				}
				foreach (var pp in updateBill)
				{
					var Billdb = book.Billing.FirstOrDefault(p => p.Id == pp.Id);

					if (Billdb != null)
					{Billdb.Amount = pp.Amount;
						Billdb.Title = pp.Title;
						
					}
					
					total+=pp.Amount;
				}
				book.total = total;
				var res = await applicationDbContext.SaveChangesAsync();
				
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

		public async Task<bool> DeleteAllBillingByBookingId(int BookingId)
		{
			var Billdb = await applicationDbContext.Billings.Where(p => p.BookingId == BookingId).ExecuteDeleteAsync();
			if (Billdb > 0) {
				var Book = await applicationDbContext.Bookings.Where(b => b.Id == BookingId).FirstOrDefaultAsync();
				Book.total = 0;
				var res = await applicationDbContext.SaveChangesAsync();
				if(res>0)
				return true;
				else
				{
					return false;
				}
			}
			else return false;
		}

		public async Task<bool> DeleteBilling(int BillingId, ClaimsPrincipal claims)
		{
			var Billdb = await applicationDbContext.Billings.Include(b => b.booking).FirstOrDefaultAsync(p => p.Id == BillingId);
			if (Billdb == null)
			{
				return false;
			}
			else
			{
				if (Billdb.booking.doctorId == userManager.GetUserId(claims))
				{
					Billdb.booking.total -= Billdb.Amount;
					await applicationDbContext.SaveChangesAsync();
					applicationDbContext.Remove(Billdb);
					await applicationDbContext.SaveChangesAsync();
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public async Task<Booking> GetBillingByBookingId(int BookingId)
		{
			var BookBillsdb = await applicationDbContext.Bookings
																.Include(p => p.patient)
																.Include(p => p.patient.address)
																.Include(d => d.doctor)
																.Include(d => d.doctor.doctor.Speciality)
																.Include(d => d.doctor.address)
																.Include(p => p.Billing)
																.Where(b => b.Id == BookingId)
																.FirstOrDefaultAsync();
			if (BookBillsdb == null)
			{
				return null;
			}
			else
			{
				return BookBillsdb;
			}
		}
	}
}
