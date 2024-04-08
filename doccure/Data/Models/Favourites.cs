namespace doccure.Data.Models
{
	public class Favourites
	{
		public int Id { get; set; }
		public string? patientId { get; set; }
		public Applicationuser? patient { get; set; }
		public string? doctorId { get; set; }
		public Applicationuser? doctor { get; set; }
	}
}
