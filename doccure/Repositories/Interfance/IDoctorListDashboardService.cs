using doccure.Data.Models;
using doccure.Repositories.Implementaion;

namespace doccure.Repositories.Interfance
{
	public interface IDoctorListDashboardService
	{
		public Task<List<DoctorDTO>> GetAllUsers(bool pagination);
	}
}
