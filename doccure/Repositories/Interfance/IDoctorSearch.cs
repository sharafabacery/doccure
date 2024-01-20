using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using doccure.Data.Models;
namespace doccure.Repositories.Interfance
{
    public interface IDoctorSearch
	{
		 Task<DoctorsSearch> SearchDoctors(DoctorSearchBarRequest doctorSearchBarRequest);
		Task<DoctorsSearch> SearchDoctors(DoctorSearch doctorSearchBarRequest);
		Task<DoctorData> GetDoctorData(string Id);
	}
}
