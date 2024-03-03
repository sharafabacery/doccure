using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface ILastAppointmentofPatient
	{
		public Task<Booking> LastAppoitment(ClaimsPrincipal claims, string patientId);
	}
}
