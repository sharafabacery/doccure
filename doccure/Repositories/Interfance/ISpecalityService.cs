using doccure.Data.Models;
using doccure.Data.RequestModels;

namespace doccure.Repositories.Interfance
{
	public interface ISpecalityService
	{
		public Task<List<Speciality>> GetSpecialities();
		public Task<Speciality>AddSpeciality(SpecialityRequest speciality);
		public Task<bool> RemoveSpeciality(int specialityId);
		public Task<Speciality>EditSpeciality(SpecialityRequest speciality);

	}
}
