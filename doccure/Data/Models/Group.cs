namespace doccure.Data.Models
{
	public class Group
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Applicationuser> user { get; } = new List<Applicationuser>(); // Collection navigation containing dependents 
	}
}
