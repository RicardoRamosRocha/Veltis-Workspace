using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Agents;

public sealed class AgentExecution : TenantEntity
{
    public Guid UserId { get; set; }
    public Guid WorkspaceId { get; set; }
    public Guid ProfessionId { get; set; }
    public Guid AgentId { get; set; }
    public Agent? Agent { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string? Response { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public TimeSpan ExecutionTime { get; set; }
    public int TokensInput { get; set; }
    public int TokensOutput { get; set; }
    public decimal EstimatedCost { get; set; }
}
