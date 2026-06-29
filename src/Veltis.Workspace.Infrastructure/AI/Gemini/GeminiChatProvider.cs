using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Veltis.Workspace.Application.AI;

namespace Veltis.Workspace.Infrastructure.AI.Gemini;

public sealed class GeminiChatProvider : IAiChatProvider
{
    private const string ProviderName = "Gemini";
    private readonly HttpClient _httpClient;
    private readonly IOptions<AiProviderOptions> _options;
    private readonly ILogger<GeminiChatProvider> _logger;

    public GeminiChatProvider(
        HttpClient httpClient,
        IOptions<AiProviderOptions> options,
        ILogger<GeminiChatProvider> logger)
    {
        _httpClient = httpClient;
        _options = options;
        _logger = logger;
    }

    public async Task<AiChatResponse> ExecuteAsync(AiChatRequest request, CancellationToken cancellationToken = default)
    {
        var settings = _options.Value.Gemini;

        if (string.IsNullOrWhiteSpace(settings.ApiKey))
        {
            return new AiChatResponse
            {
                Provider = ProviderName,
                Model = request.Model,
                Success = false,
                ErrorMessage = "Gemini API Key não configurada."
            };
        }

        var model = string.IsNullOrWhiteSpace(request.Model) ? settings.Model : request.Model;

        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            $"v1beta/models/{model}:generateContent?key={settings.ApiKey}");

        var userPrompt = $"""
        {request.SystemPrompt}

        {request.UserPrompt}
        """;

        var payload = new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = userPrompt }
                    }
                }
            },
            generationConfig = new
            {
                temperature = request.Temperature,
                maxOutputTokens = request.MaxTokens
            }
        };

        httpRequest.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        using var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        if (!httpResponse.IsSuccessStatusCode)
        {
            _logger.LogWarning("Gemini request failed. StatusCode={StatusCode}", (int)httpResponse.StatusCode);

            return new AiChatResponse
            {
                Provider = ProviderName,
                Model = model,
                Success = false,
                ErrorMessage = $"Gemini retornou HTTP {(int)httpResponse.StatusCode}."
            };
        }

        using var document = JsonDocument.Parse(responseBody);
        var root = document.RootElement;

        var content = root
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString() ?? string.Empty;

        var inputTokens = TryGetUsageToken(root, "promptTokenCount");
        var outputTokens = TryGetUsageToken(root, "candidatesTokenCount");

        return new AiChatResponse
        {
            Content = content,
            Provider = ProviderName,
            Model = model,
            InputTokens = inputTokens,
            OutputTokens = outputTokens,
            EstimatedCost = 0m,
            Success = true
        };
    }

    private static int TryGetUsageToken(JsonElement root, string propertyName)
    {
        if (root.TryGetProperty("usageMetadata", out var usage) &&
            usage.TryGetProperty(propertyName, out var tokenElement) &&
            tokenElement.TryGetInt32(out var tokens))
        {
            return tokens;
        }

        return 0;
    }
}