using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.EntityFrameworkCore;
using static doccure.Program;

namespace doccure.Repositories.Implementaion
{
	public class SpecalityService : ISpecalityService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly IFileService fileService;

		public SpecalityService(ApplicationDbContext applicationDbContext, ServiceResolver2 serviceAccessor)
		{
			this.applicationDbContext = applicationDbContext;
			this.fileService = serviceAccessor("Image");
		}

		public async Task<Speciality> AddSpeciality(SpecialityRequest speciality)
		{
			string UserImage = fileService.SaveFile(speciality.ImageFile);
			if (UserImage == "")
			{
				return null;
			}
			else
			{
				speciality.Speciality.image = UserImage;
			}
			var specialitydb = await applicationDbContext.Speciality.AddAsync(speciality.Speciality);
			var Inserted = await applicationDbContext.SaveChangesAsync();
			if (Inserted > 0) return speciality.Speciality;
			return null;

		}

		public async Task<Speciality> EditSpeciality(SpecialityRequest speciality)
		{
			var specialitydb = await applicationDbContext.Speciality.Where(s=>s.Id== speciality.Id).FirstOrDefaultAsync();
			if(specialitydb != null)
			{
				string UserImage = fileService.SaveFile(speciality.ImageFile);
				if (UserImage == "")
				{
					specialitydb.image = specialitydb.image;
				}
				else
				{
					specialitydb.image = UserImage;
				}
				specialitydb.Name = speciality.Speciality.Name;
				var result = await applicationDbContext.SaveChangesAsync();
				if (result > 0) return specialitydb;
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		public async Task<List<Speciality>> GetSpecialities()
		{
			List<Speciality> specialities = await applicationDbContext.Speciality.ToListAsync();

			return specialities;
		}

		public async Task<bool> RemoveSpeciality(int specialityId)
		{
			var speciality = await applicationDbContext.Speciality.Where(e => e.Id == specialityId).ExecuteDeleteAsync();
			return speciality>0;
		}
	}
}
