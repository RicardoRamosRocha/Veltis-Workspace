namespace Veltis.Workspace.Application.AI;

public sealed class AiChatRequest
{
    public string SystemPrompt { get; set; } = string.Empty;
    public string UserPrompt { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal Temperature { get; set; } = 0.2m;
    public int MaxTokens { get; set; } = 2000;
}
