using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;

namespace doccure.Repositories.Interfance
{
	public interface IDoctorSearch
	{
		 Task<List<DoctorSearchReturned>> SearchDoctors(DoctorSearchBarRequest doctorSearchBarRequest);
	}
}
