namespace doccure.Data.Models
{
	public class Review
	{
		public int Id { get; set; }
		public int stars { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int BookingId { get; set; }
		public DateTime createdDate { get; set; }
		public Booking booking { get; set; }
		public ICollection<Comment> Comments { get; } = new List<Comment>(); // Collection navigation containing dependents
	}
}
