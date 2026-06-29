using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Settings;

public sealed class WorkspaceSetting : TenantEntity
{
    public Guid WorkspaceId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
}
