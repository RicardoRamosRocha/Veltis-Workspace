using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Permissions;

public sealed class Permission : BaseEntity
{
    public Guid PermissionGroupId { get; set; }
    public PermissionGroup? PermissionGroup { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
