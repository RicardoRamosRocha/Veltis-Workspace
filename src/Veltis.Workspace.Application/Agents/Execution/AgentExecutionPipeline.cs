using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Veltis.Workspace.Application.AI;

namespace Veltis.Workspace.Application.Agents.Execution;

public sealed class AgentExecutionPipeline : IAgentExecutionPipeline
{
    private readonly IExecutionPromptBuilder _promptBuilder;
    private readonly IAiChatProvider _aiChatProvider;
    private readonly IOptions<AiProviderOptions> _options;
    private readonly ILogger<AgentExecutionPipeline> _logger;

    public AgentExecutionPipeline(
        IExecutionPromptBuilder promptBuilder,
        IAiChatProvider aiChatProvider,
        IOptions<AiProviderOptions> options,
        ILogger<AgentExecutionPipeline> logger)
    {
        _promptBuilder = promptBuilder;
        _aiChatProvider = aiChatProvider;
        _options = options;
        _logger = logger;
    }

    public async Task<AgentExecutionResult> ExecuteAsync(
        AgentExecutionRequest request,
        CancellationToken cancellationToken = default)
    {
        string systemPrompt = "Você é um agente profissional do Veltis Workspace.";
        string instructions = "Transforme os dados enviados em um documento profissional.";
        string prompt = _promptBuilder.Build(request, systemPrompt, instructions);

        AiProviderOptions options = _options.Value;

        string providerName = string.IsNullOrWhiteSpace(options.DefaultProvider)
            ? "OpenAI"
            : options.DefaultProvider;

        string model = GetModel(options, providerName);
        bool hasApiKey = HasApiKey(options, providerName);

        if (!hasApiKey)
        {
            string fallbackMessage =
                $"Fallback simulado: nenhuma API Key foi configurada para o provider {providerName}.";

            _logger.LogInformation("AI fallback used. Provider={Provider} Model={Model}", providerName, model);

            return new AgentExecutionResult
            {
                Success = true,
                Prompt = prompt,
                Provider = providerName,
                Model = model,
                FallbackMessage = fallbackMessage,
                Response = $"""
                Documento gerado em modo simulado.

                Esta resposta não foi criada por uma IA real porque nenhuma API Key foi configurada.

                Configure a variável de ambiente correta:

                Gemini:
                AI__Gemini__ApiKey

                OpenAI:
                AI__OpenAI__ApiKey
                """
            };
        }

        Stopwatch stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("AI execution started. Provider={Provider} Model={Model}", providerName, model);

        AiChatResponse response = await _aiChatProvider.ExecuteAsync(
            new AiChatRequest
            {
                SystemPrompt = systemPrompt,
                UserPrompt = prompt,
                Model = model,
                Temperature = 0.2m,
                MaxTokens = 2000
            },
            cancellationToken);

        stopwatch.Stop();

        _logger.LogInformation(
            "AI execution finished. Provider={Provider} Model={Model} Success={Success} ElapsedMs={ElapsedMs}",
            response.Provider,
            response.Model,
            response.Success,
            stopwatch.ElapsedMilliseconds);

        return new AgentExecutionResult
        {
            Success = response.Success,
            Prompt = prompt,
            Response = response.Content,
            Provider = response.Provider,
            Model = response.Model,
            InputTokens = response.InputTokens,
            OutputTokens = response.OutputTokens,
            EstimatedCost = response.EstimatedCost,
            Error = response.ErrorMessage
        };
    }

    private static string GetModel(AiProviderOptions options, string providerName)
    {
        if (string.Equals(providerName, "Gemini", StringComparison.OrdinalIgnoreCase))
        {
            return string.IsNullOrWhiteSpace(options.Gemini.Model)
                ? "gemini-2.5-flash"
                : options.Gemini.Model;
        }

        return string.IsNullOrWhiteSpace(options.OpenAI.Model)
            ? "gpt-4o-mini"
            : options.OpenAI.Model;
    }

    private static bool HasApiKey(AiProviderOptions options, string providerName)
    {
        if (string.Equals(providerName, "Gemini", StringComparison.OrdinalIgnoreCase))
        {
            return !string.IsNullOrWhiteSpace(options.Gemini.ApiKey);
        }

        if (string.Equals(providerName, "OpenAI", StringComparison.OrdinalIgnoreCase))
        {
            return !string.IsNullOrWhiteSpace(options.OpenAI.ApiKey);
        }

        return false;
    }
}