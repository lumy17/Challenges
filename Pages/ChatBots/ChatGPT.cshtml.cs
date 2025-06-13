using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenAI_API;
using OpenAI_API.Chat;

namespace Challenges.WebApp.Pages.ChatBots
{
    public class ChatGPTModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ChatGPTModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string UserMessage { get; set; }

        public string ResponseMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(UserMessage))
            {
                return Page();
            }

            var openAiKey = _configuration["OpenAIKey"];
            var openai = new OpenAIAPI(openAiKey);

            var chatRequest = new ChatRequest
            {
                Messages = new List<ChatMessage>
                {
                    new ChatMessage { Role = ChatMessageRole.System, TextContent = "You are a helpful assistant." },
                    new ChatMessage { Role = ChatMessageRole.User, TextContent = UserMessage }
                },
                Model = "gpt-3.5-turbo",
                MaxTokens = 500
            };

            var result = await openai.Chat.CreateChatCompletionAsync(chatRequest);
            ResponseMessage = result.Choices.FirstOrDefault()?.Message?.Content;

            return Page();
        }
    }
}
