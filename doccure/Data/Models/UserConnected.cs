namespace doccure.Data.Models
{
	public class UserConnected
	{
		public int Id { get; set; }
		public string applicationuserId { get; set; }
		public string ConnectionID { get; set; }
		public Applicationuser applicationuser { get; set; }

	}
}
