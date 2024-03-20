namespace doccure.Data.RequestModels
{
	public class MedicalRecordRequest
	{
		public int BookingId { get; set; }
		public string Description { get; set; }
		public DateTime dateTime { get; set; }
		public IFormFile? ImageFile { get; set; }

	}
}
