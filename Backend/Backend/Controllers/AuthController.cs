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
            HttpContext.Response.Cookies.Delete("UserId");
            HttpContext.Response.Cookies.Append("UserId", userId.Value.ToString(), new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true
            });

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

            HttpContext.Response.Cookies.Delete("UserId");
            return Ok(new { message = "Register successful" });

        }
        [HttpPost("verifyCookie")]
        public async Task<IActionResult> VerifyCookie()
        {
            string cookieValue = HttpContext.Request.Cookies["UserId"];
            if (cookieValue != null)
            {
                return Ok(new { cookieValue });
            }
            else return BadRequest();

        }
    }
}
