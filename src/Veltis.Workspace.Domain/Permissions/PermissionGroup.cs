using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Permissions;

public sealed class PermissionGroup : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public ICollection<Permission> Permissions { get; } = new List<Permission>();
}
