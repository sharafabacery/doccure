using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface ILastBillingService
	{
		public Task<Booking> LastBilling(ClaimsPrincipal claims, string patientId);
	}
}
