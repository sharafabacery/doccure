using doccure.Data.Models;

namespace doccure.Data.ResponseModels
{
    public class DoctorData
    {
        public Applicationuser applicationuser { get; set; }
        public List<ClinicImage> clinicImages { set; get; }
    }
}
