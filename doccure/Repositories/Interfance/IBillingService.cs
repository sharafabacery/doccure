using doccure.Data.Models;
using doccure.Data.RequestModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IBillingService
	{
		public Task<Booking> AddEditBilling(BillingRequest billingRequest);
		public Task<bool> DeleteBilling(int BillingId, ClaimsPrincipal claims);
		public Task<Booking> GetBillingByBookingId(int BookingId);
		public Task<bool> DeleteAllBillingByBookingId(int BookingId);
	}
}
