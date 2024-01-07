namespace doccure.Data.RequestModels
{
	public class DoctorSearch
	{
		public List<char> Gender { get; set; }
		public List<string> Spcialites { get; set; }
		public int? SkipRows { get; set; }
	}
}
