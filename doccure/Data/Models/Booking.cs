namespace doccure.Data.Models
{
	public enum PaymentMethod
	{
		CreditCard = 1,
		Paypal = 2,
		NotPayed=3
	}
	public class Booking
	{
		public int Id { get; set; }
		public DateTime bookingDate { get; set; }
		public double price { get; set; }
		public double videocall { get; set; }
		public double total { get; set; }
		public PaymentMethod payment { get; set; }
		public int scheduletimingId { get; set; }
		public string? patientId { get; set; }
		public Applicationuser?
			patient { get; set; } 
		public string? doctorId { get; set; }
		public Applicationuser? doctor { get; set; }

		public ScheduleTiming scheduleTiming { get; set; } = null;

	}
}
