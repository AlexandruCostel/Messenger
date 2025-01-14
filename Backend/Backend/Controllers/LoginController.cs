using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;


namespace Backend.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestM request)
        {
            bool isValid = await _authService.Login(request.UserName, request.Password);
            if (!isValid)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(new { message = "Login successful" });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequestM request)
        {

            if(await _authService.RegisterUser(request.UserName, request.Password)==true)
                return Ok(new { message = "Register successful" });
            else return BadRequest();

        }
    }
}
