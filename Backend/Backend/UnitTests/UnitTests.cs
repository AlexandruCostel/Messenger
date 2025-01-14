using Xunit;
using Backend.Services;
using Backend.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Microsoft.EntityFrameworkCore;



namespace Backend.UnitTests
{
    public class UnitTests : Controller
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;
        public UnitTests()
        {
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(_configuration)
                .AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase")))
                .AddScoped<AuthService>()
                .BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<AppDbContext>();

            _authService = serviceProvider.GetRequiredService<AuthService>();
        }

        [Fact]
        public void TestAdunare()
        {
            int rezultat = 2 + 2;
            Assert.Equal(4, rezultat);
        }

        [Fact]
        public void TestRegister()
        {
            string username = "test";
            string password = "test";

            Task<bool> task = _authService.RegisterUser(username, password);
            bool result = task.Result;
            Assert.False(result);

        }
        [Fact]
        public void TestLogin()
        {
            string username = "test";
            string password = "test";

            Task<bool> task = _authService.Login(username, password);
            bool result = task.Result;
            Assert.True(result);

        }
    }
}
