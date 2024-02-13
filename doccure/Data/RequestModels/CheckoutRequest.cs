namespace doccure.Data.RequestModels
{
	public class CheckoutRequest
	{
		public int BookingId { get; set; }
		public bool CreditCard { get; set; }
		public bool Paypal { get; set; }
	}
}
