namespace Backend.Models
{
    public class RecruitmentPost
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int AvailablePlaces {  get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
