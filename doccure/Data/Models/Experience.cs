namespace doccure.Data.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string HospitalName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Designation { get; set; }


        public Doctor doctor { get; set; } = null!;
    }
}
