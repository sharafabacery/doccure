using doccure.Data.Models;

namespace doccure.Data.RequestModels
{
	public class PrescriptionRequest
	{
		public int BookingId { get; set; }
		public List<Prescription> prescriptions{ get; set; }=new List<Prescription>();
	}
}
