using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace SmartNeighborhoodAPI.Services
{
    public class ChatService
    {
        public async Task<ApiResponse<string>> ChatAsync(string message)
        {
            var openAIOptions = new OpenAIClientOptions()
            {
                Endpoint = new Uri("https://models.inference.ai.azure.com")
            };

            var client = new ChatClient("gpt-4o", new ApiKeyCredential("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"), openAIOptions);

            var requestOptions = new ChatCompletionOptions()
            {
                Temperature = 1,
            };

            // Send only the current user message (no history)
            var response = client.CompleteChat(new List<ChatMessage>
        {
            new SystemChatMessage("You are a helpful assistant."),
            new UserChatMessage(message)
        }, requestOptions);

            var assistantResponse = response.Value.Content[0].Text;

            return ApiResponse<string>.Success(assistantResponse, "Success");
        }
    }

}
