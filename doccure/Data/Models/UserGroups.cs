namespace doccure.Data.Models
{
	public class UserGroups
	{
		public int Id { get; set; }
		public string applicationuserId { get; set; }
		public int GroupId { get; set; }
		public bool Active { get; set; }=false;
		public Group group { get; set; }
		public Applicationuser applicationuser { get; set; }
	}
}
