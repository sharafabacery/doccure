namespace doccure.Data.RequestModels
{
	public class MessageUser
	{
		public string Sender { get; set; }
		public string Reciver { get; set; }
		public string GroupName { get; set; }
		public int GroupId { set; get; }
		public int MessageId { get; set; }
	}
}
