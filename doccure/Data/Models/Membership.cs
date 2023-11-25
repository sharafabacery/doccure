namespace doccure.Data.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string description { get; set; }
       

        public Doctor doctor { get; set; } = null!;
    }
}
