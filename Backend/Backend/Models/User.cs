namespace Backend.Models
{
    public class User
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string username { get; set; }
        public string userpassword { get; set; }
        public string email { get; set; }

    }
}