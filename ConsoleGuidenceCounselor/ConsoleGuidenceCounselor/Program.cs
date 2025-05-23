#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

using System;
using System.IO;
using ConsoleGuidenceCounselor.Plugins;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;

var builder = Kernel.CreateBuilder();
var modelId = "qwen3:8b";
var endpoint = new Uri("http://localhost:11434");

builder.Services.AddOllamaChatCompletion(modelId, endpoint);

//builder.Plugins
//    .AddFromType<MyTimePlugin>()
//    .AddFromObject(new MyLightPlugin(turnedOn: true))
//    .AddFromObject(new MyAlarmPlugin("11"))
//    .AddFromObject(new AcademicInfoPlugin());
builder.Plugins.AddFromObject(new AcademicInfoPlugin());
var kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
var settings = new OllamaPromptExecutionSettings { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };

var setupPath = Path.Combine(Environment.CurrentDirectory, "SetupText.txt");
var setuptext = File.ReadAllText(setupPath);

var GradPath = Path.Combine(Environment.CurrentDirectory, "GraduationRequirements.txt");
var graduationPathways = File.ReadAllText(GradPath);

ChatMessageContent readPAGradRequirements = await chatCompletionService.GetChatMessageContentAsync(graduationPathways, settings, kernel);

ChatMessageContent setupResult = await chatCompletionService.GetChatMessageContentAsync(setuptext, settings, kernel);
writeTrimResults(setupResult);

string? input = null;
while ((input = Console.ReadLine()) is not null)
{
    Console.WriteLine();

    try
    {
        ChatMessageContent chatResult = await chatCompletionService.GetChatMessageContentAsync(input, settings, kernel);
        writeTrimResults(chatResult);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n\n> ");
    }
}


void writeTrimResults(ChatMessageContent chatMessageContent)
{
    string trimmedContent = chatMessageContent.Content.Substring(chatMessageContent.Content.IndexOf("</think") + 7);
    Console.Write($"\n>>> Result: {trimmedContent}\n\n> ");
}
