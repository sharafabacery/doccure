using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IDoctorClinicService
	{
		public Task<Applicationuser> GetDoctorClinics(ClaimsPrincipal claims);
	}
}
