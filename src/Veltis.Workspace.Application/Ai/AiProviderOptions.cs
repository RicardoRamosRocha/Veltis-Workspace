namespace Veltis.Workspace.Application.AI;

public sealed class AiProviderOptions
{
    public string DefaultProvider { get; set; } = "OpenAI";

    public OpenAiProviderSettings OpenAI { get; set; } = new();

    public GeminiProviderSettings Gemini { get; set; } = new();
}

public sealed class OpenAiProviderSettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string Model { get; set; } = "gpt-4o-mini";

    public string BaseUrl { get; set; } = "https://api.openai.com/v1";
}

public sealed class GeminiProviderSettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string Model { get; set; } = "gemini-2.5-flash";
}