using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IPatientAppiontmentProfileDoctorSide
	{
		public Task<List<Booking>> GetAllAppoitementPatientByDoctorId(ClaimsPrincipal claims,string patientId);

	}
}
