using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestM request)
        {
            Guid? userId = await _authService.Login(request.UserName, request.Password);
            if (!userId.HasValue)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("UserId", userId.Value.ToString());
            return Ok(new { message = "Login successful" });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestM request)
        {

            if(await _authService.RegisterUser(request.UserName, request.Email , request.Password) ==true)
                return Ok(new { message = "Register successful" });
            else return BadRequest();

        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            HttpContext.Session.Clear();
            return Ok(new { message = "Register successful" });

        }
        [HttpPost("verifyCookie")]
        public async Task<IActionResult> VerifyCookie()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                return Ok(new { UserId = userId });
            }
            else
            {
                return Unauthorized(new { message = "Session expired or invalid." });
            }

        }
    }
}
