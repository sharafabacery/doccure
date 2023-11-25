namespace doccure.Data.Models
{
    public class ClinicImage
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public string image { get; set; }

        public Clinic clinic { get; set; } = null!;
    }
}
