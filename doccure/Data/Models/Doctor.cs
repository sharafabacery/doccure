using System.ComponentModel.DataAnnotations;

namespace doccure.Data.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string applicationuserId { get; set; }
        public int SpecialityId { get; set; }
        public string? Specialization { get; set; }
        public string? Services { get; set; }
        public float? Price { get; set; }
        public float? VideoCall { get; set; }
        public Speciality Speciality { get; set; } = null!;
        public Applicationuser applicationuser { get; set; } = null;
        public ICollection<Education> educations { get; } = new List<Education>(); // Collection navigation containing dependents
        public ICollection<Experience> experiences { get; } = new List<Experience>(); // Collection navigation containing dependents
        public ICollection<Awards> awards { get; } = new List<Awards>(); // Collection navigation containing dependents
        public ICollection<Membership> memberships { get; } = new List<Membership>(); // Collection navigation containing dependents
        public ICollection<Clinic> clinics { get; } = new List<Clinic>(); // Collection navigation containing dependents
    }
}
