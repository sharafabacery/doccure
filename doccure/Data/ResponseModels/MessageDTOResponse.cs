namespace doccure.Data.ResponseModels
{
	public class MessageDTOResponse
	{
		public int Id { get; set; }
		public string? Message { get; set; }
		public string CreatedTime { get; set; }
		public bool Read { get; set; }
		public bool SoftDelete { get; set; }
		public string? File { get; set; }
		public string senderId { get; set; }
		public string receiverId { get; set; }
	}
}
