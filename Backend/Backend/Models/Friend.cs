namespace Backend.Models
{
    public class Friend
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public User User { get; set; }
        public User FriendUser { get; set; }
    }
}
