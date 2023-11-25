namespace doccure.Data.Models
{
    public class Awards
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string award { get; set; }
        public DateTime year { get; set; }
      

        public Doctor doctor { get; set; } = null!;
    }
}
