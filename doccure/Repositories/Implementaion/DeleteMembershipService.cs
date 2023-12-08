using doccure.Data.Models;
using doccure.Data;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class DeleteMembershipService : IDeleteMembershipService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public DeleteMembershipService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<bool> DeleteMembershipAsync(int id, ClaimsPrincipal user)
		{
			var User = await userManager.Users.Include(a => a.doctor)
									.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			var membership = await applicationDbContext.Memberships.FirstOrDefaultAsync(e => e.Id == id);
			if (User == null || membership == null)
			{
				return false;
			}
			else
			{
				if (membership.DoctorId == User.doctor.Id)
				{
					applicationDbContext.Remove(membership);
					await applicationDbContext.SaveChangesAsync();
					return true;
				}
				else
				{
					return false;
				}


			}
		}
	}
}
