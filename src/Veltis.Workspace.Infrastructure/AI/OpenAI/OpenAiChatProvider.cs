using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Veltis.Workspace.Application.AI;

namespace Veltis.Workspace.Infrastructure.AI.OpenAI;

public sealed class OpenAiChatProvider : IAiChatProvider
{
    private const string ProviderName = "OpenAI";
    private readonly HttpClient _httpClient;
    private readonly IOptions<AiProviderOptions> _options;
    private readonly ILogger<OpenAiChatProvider> _logger;

    public OpenAiChatProvider(
        HttpClient httpClient,
        IOptions<AiProviderOptions> options,
        ILogger<OpenAiChatProvider> logger)
    {
        _httpClient = httpClient;
        _options = options;
        _logger = logger;
    }

    public async Task<AiChatResponse> ExecuteAsync(AiChatRequest request, CancellationToken cancellationToken = default)
    {
        OpenAiProviderSettings settings = _options.Value.OpenAI;

        if (string.IsNullOrWhiteSpace(settings.ApiKey))
        {
            return new AiChatResponse
            {
                Provider = ProviderName,
                Model = request.Model,
                Success = false,
                ErrorMessage = "OpenAI API Key não configurada."
            };
        }

        using HttpRequestMessage httpRequest = new(HttpMethod.Post, "chat/completions");
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", settings.ApiKey);

        var payload = new
        {
            model = string.IsNullOrWhiteSpace(request.Model) ? settings.Model : request.Model,
            messages = new[]
            {
                new { role = "system", content = request.SystemPrompt },
                new { role = "user", content = request.UserPrompt }
            },
            temperature = request.Temperature,
            max_tokens = request.MaxTokens
        };

        httpRequest.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        string responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        if (!httpResponse.IsSuccessStatusCode)
        {
            _logger.LogWarning("OpenAI request failed. StatusCode={StatusCode}", (int)httpResponse.StatusCode);
            return new AiChatResponse
            {
                Provider = ProviderName,
                Model = payload.model,
                Success = false,
                ErrorMessage = $"OpenAI retornou HTTP {(int)httpResponse.StatusCode}."
            };
        }

        using JsonDocument document = JsonDocument.Parse(responseBody);
        JsonElement root = document.RootElement;
        string content = root.GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? string.Empty;

        int inputTokens = TryGetToken(root, "prompt_tokens");
        int outputTokens = TryGetToken(root, "completion_tokens");

        return new AiChatResponse
        {
            Content = content,
            Provider = ProviderName,
            Model = payload.model,
            InputTokens = inputTokens,
            OutputTokens = outputTokens,
            EstimatedCost = EstimateCost(payload.model, inputTokens, outputTokens),
            Success = true
        };
    }

    private static int TryGetToken(JsonElement root, string propertyName)
    {
        if (root.TryGetProperty("usage", out JsonElement usage) &&
            usage.TryGetProperty(propertyName, out JsonElement tokenElement) &&
            tokenElement.TryGetInt32(out int tokens))
        {
            return tokens;
        }

        return 0;
    }

    private static decimal EstimateCost(string model, int inputTokens, int outputTokens)
    {
        if (!model.Equals("gpt-4o-mini", StringComparison.OrdinalIgnoreCase))
        {
            return 0m;
        }

        decimal inputCost = inputTokens / 1_000_000m * 0.15m;
        decimal outputCost = outputTokens / 1_000_000m * 0.60m;
        return decimal.Round(inputCost + outputCost, 6);
    }
}
