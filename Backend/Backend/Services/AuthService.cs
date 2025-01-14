using Backend.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Backend.Services
{
    public class AuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext)  
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Login(string username, string password)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.username == username);

            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.userpassword);
        }
        public async Task<bool> VerifyUserExists(string username)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.username == username);

            if (user == null)
                return true;

            return false;
        }

        public async Task<bool> RegisterUser(string username, string password)
        {
            bool isValid = await this.VerifyUserExists(username);
            if (!isValid)
            {
                return false;
            }

            string cryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new users
            {
                username = username,
                userpassword = cryptedPassword
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
