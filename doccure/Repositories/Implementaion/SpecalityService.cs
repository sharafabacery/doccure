using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class SpecalityService : ISpecalityService
	{
		private readonly ApplicationDbContext applicationDbContext;

		public SpecalityService(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<List<Speciality>> GetSpecialities()
		{
			List<Speciality> specialities = await applicationDbContext.Speciality.ToListAsync();

			return specialities;
		}
	}
}
