using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Common;

namespace Backend.Controllers
{
    public class FriendshipsController : Controller
    {
        private readonly FriendshipService _friendshipService;

        public FriendshipsController(FriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet("friend-requests")]
        public async Task<IActionResult> GetFriendRequests()
        {
            var userIdCookie = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdCookie))
            {
                return BadRequest("User ID is missing.");
            }
            var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            var requests = await _friendshipService.GetFriendRequests(userId);
            return Ok(requests);
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetFriends()
        {
            var userIdCookie = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdCookie))
            {
                return BadRequest("User ID is missing.");
            }

            var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            var friends = await _friendshipService.GetFriends(userId);
            return Ok(friends);
        }


        [HttpPost("send-request")]
        public async Task<IActionResult> SendRequest([FromBody] UserData data)
        {
            var userIdCookie = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdCookie))
            {
                return BadRequest("User ID is missing.");
            }
            var senderId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            var receiverName = data.FriendName;
            await _friendshipService.SendFriendRequest(senderId, receiverName);
            return Ok(new { message = "Friend request sent." });
        }

        [HttpPost("accept-request")]
        public async Task<IActionResult> AcceptRequest([FromBody] UserData data)
        {
            var userIdCookie = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdCookie))
            {
                return BadRequest("User ID is missing.");
            }
            var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            var friendName = data.FriendName;
            await _friendshipService.AcceptFriendRequest(userId, friendName);
            return Ok(new { message = "Friend request accepted." });
        }

        [HttpDelete("delete-request")]
        public async Task<IActionResult> DeleteRequest([FromBody] UserData data)
        {
            var userIdCookie = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdCookie))
            {
                return BadRequest("User ID is missing.");
            }
            var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            var friendName = data.FriendName;
            await _friendshipService.DeleteFriendRequest(userId, friendName);
            return Ok(new { message = "Friend request deleted." });
        }

        [HttpDelete("delete-friend")]
        public async Task<IActionResult> DeleteFriend([FromBody] UserData data)
        {
            var userIdCookie = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdCookie))
            {
                return BadRequest("User ID is missing.");
            }
            var senderId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            var friendName = data.FriendName;
            await _friendshipService.DeleteFriend(senderId, friendName);
            return Ok(new { message = "Friend removed." });
        }

    }
}
