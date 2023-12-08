using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class DeleteExperienceService : IDeleteExperience
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public DeleteExperienceService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<bool> DeleteExperienceAsync(int id, ClaimsPrincipal user)
		{

			var User = await userManager.Users.Include(a => a.doctor)
									.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			var experience = await applicationDbContext.Experience.FirstOrDefaultAsync(e => e.Id == id);

			if (User == null||experience==null)
			{
				return false;
			}
			else
			{
				if (experience.DoctorId == User.doctor.Id)
				{
					applicationDbContext.Remove(experience);
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
