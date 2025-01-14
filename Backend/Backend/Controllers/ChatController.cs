using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/chat")]
    public class ChatController : ControllerBase
    {
        private static readonly Dictionary<Guid, HashSet<WebSocket>> _chatSockets = new();

        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("connect")]
        public async Task GetWebSocket([FromQuery] string user2Name)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var sender = HttpContext.Session.GetString("UserId");
                if (sender != null)
                {
                    var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    var userId1 = Guid.Parse(HttpContext.Session.GetString("UserId"));
                    var chatId = await _chatService.GetOrCreateChatIdAsync(userId1, user2Name);
                    if (!_chatSockets.ContainsKey(chatId))
                    {
                        _chatSockets[chatId] = new HashSet<WebSocket>();
                    }
                    _chatSockets[chatId].Add(webSocket);
                    await HandleMessages(webSocket, chatId);
                }
                else
                {
                    HttpContext.Response.StatusCode = 400;
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        private async Task HandleMessages(WebSocket webSocket, Guid chatId)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result;
            do
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var senderId = Guid.Parse(HttpContext.Session.GetString("UserId"));

                    _chatService.SaveSentMessage(chatId, senderId, message);
                    await BroadcastMessage(message, chatId,webSocket);
                }
            }
            while (!result.CloseStatus.HasValue);

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            _chatSockets[chatId].Remove(webSocket);
        }


        private async Task BroadcastMessage(string message, Guid chatId , WebSocket sender)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);

            var tasks = _chatSockets[chatId]
                .Where(webSocket => webSocket != sender)
                .Select(webSocket => webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None));

            await Task.WhenAll(tasks);
        }

    }
}