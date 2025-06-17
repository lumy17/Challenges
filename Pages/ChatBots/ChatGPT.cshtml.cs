using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenAI_API;
using OpenAI_API.Chat;

namespace Challenges.WebApp.Pages.ChatBots
{
	public class ChatGPTModel : PageModel
	{
		private readonly OpenAIAPI _openAiApi;
		private readonly IConfiguration _configuration;
		public ChatGPTModel(OpenAIAPI openAiApi, IConfiguration configuration)
		{
			_openAiApi = openAiApi;
			_configuration = configuration;
		}

		public string OutputResult { get; set; } = "";
		public async Task<IActionResult> OnPostAsync()
		{

			var query = Request.Form["msg"];

			var openAiKey = _configuration["OpenAIKey"];
			if (string.IsNullOrEmpty(openAiKey))
			{
				return new JsonResult("API Key is missing.");
			}

			var openai = new OpenAIAPI(openAiKey);
			var chatRequest = new ChatRequest
			{
				Messages = new List<ChatMessage>
				{
					new ChatMessage { Role = ChatMessageRole.System, TextContent = "You are a helpful assistant. Please keep your answers concise." },
					new ChatMessage { Role = ChatMessageRole.User, TextContent = query }
				},
				Model = "gpt-3.5-turbo",
				MaxTokens = 1028
			};

			var chatResponse = await openai.Chat.CreateChatCompletionAsync(chatRequest);
			var response = chatResponse.Choices.FirstOrDefault()?.Message?.Content;

			return new JsonResult(response ?? "Sorry, I couldn't generate a response.");
		}
	}
}