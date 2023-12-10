namespace doccure.Data.Models
{
	public class ScheduleTiming
	{
		public int Id { get; set; }
		public DateTime DateSchedule { get; set; }

		public int Day { get; set; }
		public string Time { get; set; }
		
		public int ClinicId { get; set; }
		public Clinic Clinic { get; set; } = null!;

		

	}
}
