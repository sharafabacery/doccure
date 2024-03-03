using doccure.Data.Models;
using doccure.Data.RequestModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IPrescriptionService
	{
		public Task<Booking> AddEditPrescription(PrescriptionRequest prescriptionRequest);
		public Task<bool> DeletePrescription(int PrescriptionId, ClaimsPrincipal claims);
		public Task<List<Prescription>> GetPrescriptionsByBookingId(int BookingId);
		public Task<bool> DeleteAllPrescriptionByBookingId(int BookingId);
	}
}
