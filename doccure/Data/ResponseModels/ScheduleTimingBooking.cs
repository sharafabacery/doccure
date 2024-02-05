using System.ComponentModel.DataAnnotations.Schema;

namespace doccure.Data.ResponseModels
{
	[NotMapped]
	public class ScheduleTimingBooking
	{
		public int Id { get; set; }

		public int Day { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public string BookingDate { get; set; }
		public string BookingDateDay { get; set;}
	}
}
