namespace SmartNeighborhoodAPI.Controllers.V1
{
    public class ChatController : AppControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Chat(string message)
        {
            return Response(await _chatService.ChatAsync(message));
        }
    }

}
