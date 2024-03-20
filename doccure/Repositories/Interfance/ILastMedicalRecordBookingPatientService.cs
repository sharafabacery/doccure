using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface ILastMedicalRecordBookingPatientService
	{
		public Task<Booking> LastMedicalRecord(ClaimsPrincipal claims, string patientId);
	}
}
