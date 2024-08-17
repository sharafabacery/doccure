namespace doccure.Data.RequestModels
{
	public class MessageQueryRequest
	{
		public string Sender { get; set; }
		public string Reciver { get; set; }
		public DateTime? Date { get; set; }
		public string DateString { get; set; }
	}
}
