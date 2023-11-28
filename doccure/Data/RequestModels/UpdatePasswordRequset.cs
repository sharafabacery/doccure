namespace doccure.Data.RequestModels
{
	public class UpdatePasswordRequset
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set;
		}
	}
}
