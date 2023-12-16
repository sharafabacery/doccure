using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class DoctorClinicService : IDoctorClinicService
	{
		private readonly UserManager<Applicationuser> userManager;

		public DoctorClinicService(UserManager<Applicationuser> userManager)
		{
			this.userManager = userManager;
		}
		public async Task<Applicationuser> GetDoctorClinics(ClaimsPrincipal user)
		{
			var UserClinics = await userManager.Users.Include(a => a.doctor).Include(a => a.doctor.Speciality).Include(a => a.doctor.clinics).FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			if (UserClinics == null || UserClinics.doctor.clinics.Count == 0)
			{
				return null;
			}
			else
			{
				return UserClinics;
			}
			{

			}
		}
	}
}
