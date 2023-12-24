using doccure.Data.Models;

namespace doccure.Data.RequestModels
{
	public class ScheduleTimingSlotRequest
	{
		public int Day { get; set; }
		public int? ClinicId { get; set; } 
		public List<ScheduleTiming> scheduleTimings { get; set; } = new List<ScheduleTiming>();
	}
}
