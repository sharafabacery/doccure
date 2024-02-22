namespace doccure.Data.Models
{
	public class Billing
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public float Amount { get; set; }
		public int BookingId { get; set; }
		public Booking booking { get; set; }

	}
}
