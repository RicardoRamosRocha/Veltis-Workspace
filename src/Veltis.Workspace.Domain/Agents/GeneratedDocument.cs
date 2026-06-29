using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Agents;

public sealed class GeneratedDocument : TenantEntity
{
    public Guid WorkspaceId { get; set; }
    public Guid ExecutionId { get; set; }
    public AgentExecution? Execution { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Format { get; set; } = "markdown";
    public int Version { get; set; } = 1;
    public GeneratedDocumentStatus Status { get; set; } = GeneratedDocumentStatus.Draft;
}
