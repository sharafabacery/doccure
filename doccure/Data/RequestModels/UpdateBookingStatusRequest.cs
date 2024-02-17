using doccure.Data.Models;

namespace doccure.Data.RequestModels
{
	public class UpdateBookingStatusRequest
	{
		public int BookingId {  set;get; }
		public int Status {  set; get;}

		
	}
}
