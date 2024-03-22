using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class LastBillingService : ILastBillingService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public LastBillingService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}
		public Task<Booking> LastBilling(ClaimsPrincipal claims, string patientId)
		{
			throw new NotImplementedException();
		}
	}
}
