namespace doccure.Data.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int DoctorId { get; set; }
        public Doctor doctor { get; set; } = null!;
        public ICollection<ClinicImage> clinicImages { get; } = new List<ClinicImage>(); // Collection navigation containing dependents

    }
}
