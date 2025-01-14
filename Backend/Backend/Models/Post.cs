namespace Backend.Models
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
