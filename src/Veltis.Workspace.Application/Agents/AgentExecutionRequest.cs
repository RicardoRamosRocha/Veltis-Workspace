namespace Veltis.Workspace.Application.Agents;

public sealed class AgentExecutionRequest
{
    public Guid UserId { get; set; }
    public Guid WorkspaceId { get; set; }
    public Guid ProfessionId { get; set; }
    public Guid AgentId { get; set; }
    public IReadOnlyDictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
    public IReadOnlyDictionary<string, string> UserPreferences { get; set; } = new Dictionary<string, string>();
}
