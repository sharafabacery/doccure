using doccure.Repositories.Implementaion;

namespace doccure.Repositories.Interfance
{
	public interface IPatientListService
	{
		public Task<List<PatientDTO>> GetAllUsers(bool pagination);
	}
}
