namespace doccure.Repositories.Interfance
{
	public interface IStaticticsService
	{
		public Task<int> GetUserCountByRole(string role);
		public Task<int> GetAppoinmentCount();
		public Task<double> GetRevnueTotal();
		public Task<List<KeyValuePair<string, double>>> Revenue();
		public Task<List<KeyValuePair<string, int>>> GetUsersYears();
	}
}
