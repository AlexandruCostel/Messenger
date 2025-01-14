using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public class ContentService
    {
        private readonly AppDbContext _dbContext;

        public ContentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Post>> GetFriendPosts(Guid userId, int limit = 10)
        {
            var friendIds = await _dbContext.Friend
                .Where(f => f.UserId == userId)
                .Select(f => f.FriendId)
                .ToListAsync();

            var posts = await _dbContext.Post
                .Where(p => friendIds.Contains(p.UserId))
                .OrderByDescending(p => p.Date)
                .Take(limit)
                .ToListAsync();

            return posts;
        }
    }
}
