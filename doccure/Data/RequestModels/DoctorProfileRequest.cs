using doccure.Data.Models;

namespace doccure.Data.RequestModels
{
	public class DoctorProfileRequest:UserProfileRequest
	{
		public string UserName { get; set; }
		public char gender { get; set; }
		public string AboutMe { get; set; }
		public int SpecialityId { get; set; }
		public Clinic Clinic { get; set; }
		public float price { get; set; }
		public float videocall { get; set; }
		public string? Specialization { get; set; }
		public string? Services { get; set; }
		public List<IFormFile>? ClinicImages { set; get; }
		//public Education? education { get; set; }
		public List<Education> Educations { get; set; }=new List<Education>();
		public List<Experience> Experience { get; set; } = new List<Experience>();
		public List<Awards> Awards { get; set; } = new List<Awards>();
		public List<Membership> Membership { get; set; } = new List<Membership>();



	}
}
