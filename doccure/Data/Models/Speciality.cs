using Microsoft.Extensions.Hosting;

namespace doccure.Data.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? image {get;set;}
        public ICollection<Doctor> doctors { get; } = new List<Doctor>(); // Collection navigation containing dependents
    }
}
