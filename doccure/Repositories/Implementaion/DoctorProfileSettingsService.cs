using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class DoctorProfileSettingsService : IUserProfileSettingsService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ApplicationDbContext applicationDbContext;

		public DoctorProfileSettingsService(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Applicationuser> GetUserData(ClaimsPrincipal user)
		{
			var Doctor = await userManager.Users.Include(a => a.address).Include(a=>a.doctor).Include(a=>a.doctor.educations).Include(a=>a.doctor.experiences).Include(a=>a.doctor.awards).Include(a=>a.doctor.memberships).Include(a=>a.doctor.clinics).FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			return Doctor;
			//throw new NotImplementedException();
		}

		public Task<Applicationuser> UpdatePassword(UpdatePasswordRequset updatePasswordRequset, ClaimsPrincipal userClamis)
		{
			throw new NotImplementedException();
		}

		public Task<Applicationuser> UpdateUserData(UserProfileRequest user, ClaimsPrincipal userClamis)
		{
			throw new NotImplementedException();
		}
	}
}
