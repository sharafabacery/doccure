using doccure.Repositories.Implementaion;

namespace doccure.Repositories.Interfance
{
	public interface IAppointmentListService
	{
		public Task<List<AppointmentDTO>> GetAllAppointments(bool pagination);
	}
}
