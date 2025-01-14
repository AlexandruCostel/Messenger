using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Text.Json;

namespace Backend.Services
{
    public class ChatService
    {
        private readonly AppDbContext _dbContext;

        public ChatService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> GetOrCreateChatIdAsync(Guid user1Id, string user2Name)
        {
            var user2Id = await _dbContext.User
                .Where(u => u.username == user2Name)
                .Select(u => u.id)
                .FirstOrDefaultAsync();

            var sortedUserIds = new[] { user1Id, user2Id }.OrderBy(id => id).ToArray();
            var chat = await _dbContext.Chat
                .FirstOrDefaultAsync(c => (c.User1Id == sortedUserIds[0] && c.User2Id == sortedUserIds[1]) ||
                                          (c.User1Id == sortedUserIds[1] && c.User2Id == sortedUserIds[0]));

            if (chat != null)
            {
                return chat.Id;
            }

            var newChat = new Chat
            {
                Id = Guid.NewGuid(),
                User1Id = sortedUserIds[0],
                User2Id = sortedUserIds[1]
            };

            _dbContext.Chat.Add(newChat);
            await _dbContext.SaveChangesAsync();

            return newChat.Id;
        }
        public async void SaveSentMessage(Guid chatId, Guid senderId , string text)
        {
            string content= ExtractMessage(text);

            var newMessage = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                SenderId = senderId,
                Content = content,
                SentAt = DateTime.UtcNow
            };

            _dbContext.Message.Add(newMessage);
            await _dbContext.SaveChangesAsync();

            return ;
        }

        public string ExtractMessage(string jsonString)
        {
            var jsonDocument = JsonDocument.Parse(jsonString);
            if (jsonDocument.RootElement.TryGetProperty("message", out var messageElement))
            {
                return messageElement.GetString();
            }
            return null;
        }

        public async Task<List<Message>> GetLastMessagesAsync(Guid chatId, int limit = 30)
        {
            return await _dbContext.Message
                .Where(m => m.ChatId == chatId)
                .OrderByDescending(m => m.SentAt)
                .Take(limit)
                .ToListAsync();
        }


    }
}
