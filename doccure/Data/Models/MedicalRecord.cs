namespace doccure.Data.Models
{
	public class MedicalRecord
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }

		public string? Description { get; set; }
		public string FilePath { get; set; }
		public int BookingId { get; set; }
		public Booking booking { get; set; }
	}
}
