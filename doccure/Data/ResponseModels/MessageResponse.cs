using doccure.Data.Models;

namespace doccure.Data.ResponseModels
{
	public class MessageResponse
	{
		public Messages Message { get; set; }
		public UserConnected UserConnected { get; set; }
	}
}
