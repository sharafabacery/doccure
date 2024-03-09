namespace doccure.Data.Models
{
	
	public class Prescription
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public int Days { get; set; }
		public bool Morning { get; set; }
		public bool Afternoon { get; set; }
		public bool Evening { get; set; }
		public bool Night { get; set; }
		public int BookingId { get; set; }
		public Booking booking { get; set; }
	}
}
