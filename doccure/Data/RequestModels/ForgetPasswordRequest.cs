namespace doccure.Data.RequestModels
{
	public class ForgetPasswordRequest
	{
		public string Token { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfimPassword { get; set; }
	}
}
