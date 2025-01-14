using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSkill>()
                .HasKey(us => new { us.UserId, us.SkillId });

            modelBuilder.Entity<UserBusiness>()
                .HasKey(ub => new { ub.UserId, ub.BusinessId });

            modelBuilder.Entity<Friend>()
                .HasKey(f => new { f.UserId, f.FriendId });

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendUser)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Sender)
                .WithMany()
                .HasForeignKey(fr => fr.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Receiver)
                .WithMany()
                .HasForeignKey(fr => fr.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasIndex(fr => new { fr.SenderId, fr.ReceiverId })
                .IsUnique();

        }

        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<RecruitmentPost> RecruitmentPost { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<UserBusiness> UserBusiness { get; set; }
        public DbSet<UserSkill> UserSkill { get; set; }
        public DbSet<Friend> Friend { get; set; }
        public DbSet<FriendRequest> FriendRequest { get; set; }

    }
}
