namespace Veltis.Workspace.Application.Agents.Execution;

public sealed class AgentExecutionResult
{
    public bool Success { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public string? Error { get; set; }
}