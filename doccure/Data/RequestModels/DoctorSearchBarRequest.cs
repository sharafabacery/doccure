namespace doccure.Data.RequestModels
{
	public class DoctorSearchBarRequest
	{
		public string Location { set; get; }
		public string SearchInput { set; get; }
		public int? SkipRows { set; get; }
	}
}
