using doccure.Data.Models;
using doccure.Data.RequestModels;

namespace doccure.Repositories.Interfance
{
	public interface IMedicalRecordService
	{
		public Task<Booking> AddEditMedicalRecord(MedicalRecordRequest medicalRecordRequest);
	}
}
