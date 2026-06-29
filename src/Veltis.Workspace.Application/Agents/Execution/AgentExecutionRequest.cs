namespace Veltis.Workspace.Application.Agents.Execution;

public sealed class AgentExecutionRequest
{
    public Guid UserId { get; set; }
    public Guid WorkspaceId { get; set; }
    public Guid ProfessionId { get; set; }
    public Guid AgentId { get; set; }
    public Dictionary<string, string> FormValues { get; set; } = new();
}