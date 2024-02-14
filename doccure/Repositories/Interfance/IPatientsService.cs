using doccure.Data.Models;
using doccure.Data.ResponseModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IPatientsService
	{
		Task<List<Patients>> GetPatientsByDoctorId(ClaimsPrincipal claimsPrincipal);
	}
}
