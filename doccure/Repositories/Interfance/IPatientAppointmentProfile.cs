using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IPatientAppointmentProfile
	{
		public Task<List<Booking>> GetAllAppoitementPatient(ClaimsPrincipal claims);
		public Task<List<Applicationuser>> LastBooking(ClaimsPrincipal claims);
	}
}
