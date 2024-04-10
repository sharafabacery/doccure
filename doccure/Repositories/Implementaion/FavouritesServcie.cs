using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static doccure.Program;

namespace doccure.Repositories.Implementaion
{
	public class FavouritesServcie : IFavouritesServcie
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;
		private readonly IUserProfileSettingsService userProfileSettingsService;

		public FavouritesServcie(ServiceResolver serviceAccessor,ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
			this.userProfileSettingsService = serviceAccessor("Patient");
		}
		public async Task<bool> CreateFavouriteDoctor(ClaimsPrincipal claims, int DoctorId)
		{
			var user = await userProfileSettingsService.GetUserData(claims);
			if (user == null)
			{
				return false;
			}
			else
			{
				var doctor = await applicationDbContext.Doctor.FirstOrDefaultAsync(e => e.Id == DoctorId);
				if(doctor == null)
				{
					return false;
				}
				var doctorFound = user.PatientFavourites.FirstOrDefault(e => e.doctorId == doctor.applicationuserId);
				if (doctorFound != null) return false;
				else
				{
					user.PatientFavourites.Add(new Favourites { doctorId = doctor.applicationuserId, patientId = user.Id });
					var result = await userManager.UpdateAsync(user);

					if (result.Succeeded)
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

		public async Task<bool> DeleteFavouriteDoctor(ClaimsPrincipal claims, int DoctorId)
		{
			var user = await userProfileSettingsService.GetUserData(claims);
			if (user == null)
			{
				return false;
			}
			else
			{
				var doctor = await applicationDbContext.Doctor.FirstOrDefaultAsync(e => e.Id == DoctorId);
				if (doctor == null)
				{
					return false;
				}

				var doctorFound = user.PatientFavourites.FirstOrDefault(e => e.doctorId == doctor.applicationuserId);
				if (doctorFound == null) return false;
				else
				{

					user.PatientFavourites.Remove(doctorFound);
					var result = await userManager.UpdateAsync(user);

					if (result.Succeeded)
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

		public async Task<List<Favourites>> GetFavouriteDoctorsByPatientId(ClaimsPrincipal claims)
		{
			var favourites = await applicationDbContext.Favourites.Include(a=>a.doctor).Include(e => e.doctor.doctor).Include(a=>a.doctor.address).Include(a=>a.doctor.doctor.Speciality).Include(e => e.doctor.address).Where(e => e.patientId == userManager.GetUserId(claims)).ToListAsync();
			return favourites;

;		}
	}
}
