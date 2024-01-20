namespace doccure.Data.RequestModels
{
	public class DoctorSearch
	{
		public List<int> Gender { get; set; }
		public List<int> Spcialites { get; set; }
		public int? SkipRows { get; set; }
	}
}
