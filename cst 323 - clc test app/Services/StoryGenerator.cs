﻿using cst_323___clc_test_app.Models;
using OpenAI_API;
using OpenAI_API.Chat;

namespace cst_323___clc_test_app.Services
{
	public static class StoryGenerator
	{
		const string openAiApiKey = ""; 

		static APIAuthentication aPIAuthentication;
		static OpenAIAPI openAiApi;

		static string CompilePrompt(string genre, string premise)
		{
			return "Tell me a " + genre + " story where " + premise;
		}

		public static async Task<Story> WriteStory(Story story)
		{
			aPIAuthentication = new APIAuthentication(openAiApiKey);
			openAiApi = new OpenAIAPI(aPIAuthentication);

			string prompt = CompilePrompt(story.genre, story.premise);

			try
			{

				string model = "gpt-3.5-turbo";
				int maxTokens = 1000;

				var completionRequest = new ChatRequest
				{
					Model = model,
					Messages = new ChatMessage[] { new ChatMessage(ChatMessageRole.User, prompt) },
					MaxTokens = maxTokens
				};

				var completionResult = await openAiApi.Chat.CreateChatCompletionAsync(completionRequest);
				story.story = completionResult.ToString();

				return story;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return null;
			}
		}
	}
}
