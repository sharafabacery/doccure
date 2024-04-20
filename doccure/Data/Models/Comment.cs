namespace doccure.Data.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public DateTime createdDate { get; set; }

		public int ReviewId { get; set; }
		public Review review { get; set; }
		public int? ParentCommentId { get; set; }
		public string? UserId { get; set; }
		public Applicationuser User { get; set; }
		public Comment? ParentComment { get; set; }
		public ICollection<Comment> subComments { get; } = new List<Comment>();
	}
}
