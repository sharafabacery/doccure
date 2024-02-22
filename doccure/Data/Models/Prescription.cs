namespace doccure.Data.Models
{
	public enum AvaiableTime
	{
		Morning=1,
		Afternoon=2,
		Evening=3,
		Night=4
	}
	public class Prescription
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public int Days { get; set; }
		public AvaiableTime Time { get; set; }
		public int BookingId { get; set; }
		public Booking booking { get; set; }
	}
}
