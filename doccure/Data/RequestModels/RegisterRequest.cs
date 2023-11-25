namespace doccure.Data.RequestModels
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string? Role { get; set; }
    }
}
