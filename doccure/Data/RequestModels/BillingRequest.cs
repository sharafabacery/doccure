using doccure.Data.Models;

namespace doccure.Data.RequestModels
{
	public class BillingRequest
	{
		public int BookingId { get; set; }
		public List<Billing> Bills { get; set; } = new List<Billing>();
	}
}
