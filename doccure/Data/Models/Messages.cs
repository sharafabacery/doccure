namespace doccure.Data.Models
{
	public class Messages
	{
		public int Id { get; set; }
		public string? Message { get; set; }
		public DateTime CreatedTime { get; set; }
		public bool Read { get; set; }
		public bool SoftDelete { get; set; }
		public string? File { get; set; }
		public string senderId { get; set; }
		public string receiverId { get; set; }
		public Applicationuser Sender { get; set; }
		public Applicationuser Receiver { get; set; }
	}
}
