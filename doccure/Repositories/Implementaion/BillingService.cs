using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
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
		public Task<Booking> AddEditBilling(BillingRequest billingRequest)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAllBillingByBookingId(int BookingId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteBilling(int BillingId, ClaimsPrincipal claims)
		{
			throw new NotImplementedException();
		}

		public Task<Booking> GetBillingByBookingId(int BookingId)
		{
			throw new NotImplementedException();
		}
	}
}
