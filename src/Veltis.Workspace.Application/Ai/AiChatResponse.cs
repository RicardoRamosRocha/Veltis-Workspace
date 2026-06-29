namespace Veltis.Workspace.Application.AI;

public sealed class AiChatResponse
{
    public string Content { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int InputTokens { get; set; }
    public int OutputTokens { get; set; }
    public decimal EstimatedCost { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
