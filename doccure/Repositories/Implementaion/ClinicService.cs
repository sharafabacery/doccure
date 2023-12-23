using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class ClinicService : IClinicService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public ClinicService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext) {
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}

		public async Task<Clinic> GetClinicById(int id, ClaimsPrincipal user)
		{
			var clinic = await  applicationDbContext.Clinics.FromSql($"SELECT C.*\r\nFROM [dbo].[Doctor] D\r\nLEFT JOIN [dbo].[Clinics] C\r\nON D.Id=C.DoctorId\r\nWHERE D.applicationuserId={userManager.GetUserId(user)} AND C.Id={id}").FirstOrDefaultAsync();
			if (clinic == null) {
				return null;
			}
			else
			{
				return clinic;
			}
		}
	}
}
