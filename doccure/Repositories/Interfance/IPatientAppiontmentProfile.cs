using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IPatientAppiontmentProfile
	{
		public Task<List<Booking>> GetAllAppoitementPatient(ClaimsPrincipal claims);

	}
}
