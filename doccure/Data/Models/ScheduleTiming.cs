namespace doccure.Data.Models
{
	public class ScheduleTiming
	{
		public int Id { get; set; }
		public DateTime DateSchedule { get; set; }

		public int Day { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		
		public int ClinicId { get; set; }
		public Clinic Clinic { get; set; } = null!;

		

	}
}
