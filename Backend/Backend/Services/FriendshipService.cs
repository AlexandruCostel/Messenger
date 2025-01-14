using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace Backend.Services
{
    public class FriendshipService
    {
        private readonly AppDbContext _context;

        public FriendshipService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetFriendRequests(Guid ReceiverId)
        {
            return await _context.FriendRequest
            .Where(fr => fr.ReceiverId == ReceiverId)
            .Select(fr => fr.Sender.username)
            .ToListAsync();
        }

        public async Task<List<string>> GetFriends(Guid userId)
        {
            var friendIds = await _context.Friend
                .Where(f => f.UserId == userId || f.FriendId == userId)
                .Select(f => f.UserId == userId ? f.FriendId : f.UserId)
                .ToListAsync();

            return await _context.User
                .Where(u => friendIds.Contains(u.id))
                .Select(u => u.username)
                .ToListAsync();
        }

        public async Task SendFriendRequest(Guid senderId, string receiverName)
        {
            var receiver = await _context.User.FirstOrDefaultAsync(u => u.username == receiverName);

            if (receiver == null)
                throw new InvalidOperationException("Receiver not found.");

            if (_context.FriendRequest.Any(fr => fr.SenderId == senderId && fr.ReceiverId == receiver.id))
                throw new InvalidOperationException("Friend request already sent.");

            var friendRequest = new FriendRequest
            {
                Id = Guid.NewGuid(),
                SenderId = senderId,
                ReceiverId = receiver.id,
                SentAt = DateTime.UtcNow
            };

            _context.FriendRequest.Add(friendRequest);
            await _context.SaveChangesAsync();
        }


        public async Task AcceptFriendRequest(Guid userId, string friendName)
        {
            var user = await _context.User.FindAsync(userId);
            var friend = await _context.User.FirstOrDefaultAsync(u => u.username == friendName);

            if (user == null || friend == null)
                throw new InvalidOperationException("User or friend not found.");

            var request = await _context.FriendRequest
                .FirstOrDefaultAsync(fr => fr.SenderId == friend.id && fr.ReceiverId == user.id);

            if (request == null) throw new InvalidOperationException("Request not found.");

            var friendship = new Friend
            {
                UserId = user.id,
                FriendId = friend.id
            };

            _context.Friend.Add(friendship);
            _context.FriendRequest.Remove(request);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteFriendRequest(Guid userId, string friendName)
        {
            var user = await _context.User.FindAsync(userId);
            var friend = await _context.User.FirstOrDefaultAsync(u => u.username == friendName);

            if (user == null || friend == null)
                throw new InvalidOperationException("User or friend not found.");

            var request = await _context.FriendRequest
                .FirstOrDefaultAsync(fr => fr.SenderId == friend.id && fr.ReceiverId == user.id);

            if (request == null) throw new InvalidOperationException("Request not found.");

            _context.FriendRequest.Remove(request);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteFriend(Guid userId, string friendName)
        {
            var friend = await _context.User.FirstOrDefaultAsync(u => u.username == friendName);

            var friendship = _context.Friend.FirstOrDefault(f =>
                (f.UserId == userId && f.FriendId == friend.id) ||
                (f.UserId == friend.id && f.FriendId == userId));

            if (friendship == null) throw new InvalidOperationException("Friendship not found.");

            _context.Friend.Remove(friendship);
            await _context.SaveChangesAsync();
        }
    }
}
