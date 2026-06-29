namespace Veltis.Workspace.Application.Agents.Execution;

public sealed class AgentExecutionResult
{
    public bool Success { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int InputTokens { get; set; }
    public int OutputTokens { get; set; }
    public decimal EstimatedCost { get; set; }
    public string? FallbackMessage { get; set; }
    public string? Error { get; set; }
}
