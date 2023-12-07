using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class DeleteEducationService : IDeleteEducation
	{
		private readonly UserManager<Applicationuser> userManager;

		public DeleteEducationService(UserManager<Applicationuser> userManager)
		{
			this.userManager = userManager;
		}
		public async Task<bool> DeleteEducationAsync(int id, ClaimsPrincipal user)
		{
			var UserEducations = await userManager.Users.Include(a => a.doctor).ThenInclude(a => a.educations)
									.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			if(UserEducations == null)
			{
				return false;
			}
			else
			{
				UserEducations?.doctor?.educations.Remove(new Education() { Id = id });
				var result = await userManager.UpdateAsync(UserEducations);
				if(result.Succeeded)
				{
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
