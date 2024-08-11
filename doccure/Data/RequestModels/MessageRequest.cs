namespace doccure.Data.RequestModels
{
	public class MessageRequest
	{
		public string Message { get; set; }
		public string GroupName { get; set; }
		public IFormFile? UploadedFile { get; set; }
		public string Reciver { get; set; }

	}
}
