using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class DeleteAwaradService : IDeleteAwardService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public DeleteAwaradService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<bool> DeleteAwardAsync(int id, ClaimsPrincipal user)
		{
			var User = await userManager.Users.Include(a => a.doctor)
									.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			var award = await applicationDbContext.Awards.FirstOrDefaultAsync(e => e.Id == id);
			if (User == null || award == null)
			{
				return false;
			}
			else
			{
				if (award.DoctorId == User.doctor.Id)
				{
					applicationDbContext.Remove(award);
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
