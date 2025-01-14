namespace Backend.Models
{
    public class FriendRequest
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public DateTime SentAt { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
