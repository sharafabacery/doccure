using doccure.Data.Models;

namespace doccure.Data.ResponseModels
{
	public class DoctorDashboardReturned
	{
		public int TotalPatients { get; set; }
		public int TotalTodayPatients { get; set; }
		public int TotalAppiontment { get; set; }
		public List<Booking> Upcoming { get; set; }
		public List<Booking> Today { get; set; }
	}
}
