namespace doccure.Data.Models
{
    public class Education
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Degree { get; set; }
        public string College { get; set; }
        public DateTime YearofCompletion { get; set; }

        public Doctor doctor { get; set; } = null!;

    }
}
