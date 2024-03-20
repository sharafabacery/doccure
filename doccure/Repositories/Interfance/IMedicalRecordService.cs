using doccure.Data.Models;
using doccure.Data.RequestModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IMedicalRecordService
	{
		public Task<Booking> AddEditMedicalRecord(MedicalRecordRequest medicalRecordRequest);
		public Task<bool> DeleteMedicalRecord(int MedicalRecordId, ClaimsPrincipal claims);
		public Task<Booking> GetMedicalRecordByBookingId(int BookingId);
	}
}
