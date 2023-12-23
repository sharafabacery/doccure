using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IClinicService
	{
		public Task<Clinic>GetClinicById(int id,ClaimsPrincipal claims);
	}
}
