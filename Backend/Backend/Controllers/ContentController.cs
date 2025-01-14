using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class ContentController : Controller
    {
        private readonly ContentService _contentService;

        public ContentController(ContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("MainPage")]
        public async Task<IActionResult> MainPage()
        {
            var userIdString = HttpContext.Request.Cookies["UserId"];
            if (userIdString != null && Guid.TryParse(userIdString, out Guid userId))
            {
                var posts = await _contentService.GetFriendPosts(userId, 10);

                if (posts == null || !posts.Any())
                {
                    return Ok(new { message = "No posts found. Add friends to see their posts." });
                }

                return Ok(posts);
            }
            else
            {
                return Unauthorized(new { message = "You are not logged in." });
            }
        }
    }
}
