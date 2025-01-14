namespace Backend.Models
{
    public class UserBusiness
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }
        public string Role { get; set; }
    }
}
