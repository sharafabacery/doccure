using doccure.Data.Models;

namespace doccure.Repositories.Interfance
{
	public interface ISpecalityService
	{
		public Task<List<Speciality>> GetSpecialities();
	}
}
