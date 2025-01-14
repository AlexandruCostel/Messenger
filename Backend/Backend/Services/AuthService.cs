using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using BCrypt.Net;
using System.Net.Mail;

namespace Backend.Services
{
    public class AuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext)  
        {
            _dbContext = dbContext;
        }

        public async Task<Guid?> Login(string username, string password)
        {
            var user = await _dbContext.User
                .FirstOrDefaultAsync(u => u.username == username);

            if (user == null)
                return null;

            return BCrypt.Net.BCrypt.Verify(password, user.userpassword) ? user.id : (Guid?)null;
        }

        public async Task<bool> VerifyUserExists(string username)
        {
            var user = await _dbContext.User
                .FirstOrDefaultAsync(u => u.username == username);

            
            if (user == null)
                return true;

            return false;
        }
        public async Task<bool> VerifyEmailExists(string email)
        {
            var Email = await _dbContext.User
                .FirstOrDefaultAsync(u => u.email == email);


            if (Email == null)
                return true;

            return false;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public async Task<bool> RegisterUser(string username, string email , string password)
        {
            bool isValid = await this.VerifyUserExists(username);
            if (!isValid)
            {
                return false;
            }
            if (!IsValidEmail(email))
            {
                return false;
            }

            isValid = await this.VerifyEmailExists(email);
            if (!isValid)
            {
                return false;
            }

            string cryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                username = username,
                email = email,
                userpassword = cryptedPassword
            };

            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
