using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using Microsoft.Extensions.Configuration;
using OpenAI_API.Completions;


namespace Challenges.WebApp.Pages
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

			var openAiKey = _configuration.GetValue<string>("OpenAiKey");

			var openai = new OpenAIAPI(openAiKey);
			CompletionRequest completionRequest = new CompletionRequest();
			completionRequest.Prompt = query;
			completionRequest.Model = Model.ChatGPTTurboInstruct;
			completionRequest.MaxTokens = 1024;

			var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

			foreach (var completion in completions.Completions)
			{
				OutputResult += completion.Text;
			}
			return new JsonResult(OutputResult);
		}
	}
}
