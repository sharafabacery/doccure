using doccure.Data.Models;
using doccure.Data.ResponseModels;

namespace doccure
{
	public class DoctorsSearch
	{
		public List<DoctorSearchReturned> doctorSearchReturneds{ set; get; }
		public List<ClinicImage> clinicImages { set; get; }
	}
}
