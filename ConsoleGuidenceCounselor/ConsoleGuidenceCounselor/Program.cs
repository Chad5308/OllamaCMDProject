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
builder.Plugins.AddFromObject(new AcademicInfoPlugin())
    .AddFromObject(new GraduationRequirements());
var kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
var settings = new OllamaPromptExecutionSettings { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };

var path = Path.Combine(Environment.CurrentDirectory, "SetupText.txt");
var setuptext = File.ReadAllText(path);


ChatMessageContent setupResult = await chatCompletionService.GetChatMessageContentAsync(setuptext, settings, kernel);
Console.Write($"\n>>> Result: {setupResult}\n\n> ");

string? input = null;
while ((input = Console.ReadLine()) is not null)
{
    Console.WriteLine();

    try
    {
        ChatMessageContent chatResult = await chatCompletionService.GetChatMessageContentAsync(input, settings, kernel);
        Console.Write($"\n>>> Result: {chatResult}\n\n> ");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n\n> ");
    }
}
