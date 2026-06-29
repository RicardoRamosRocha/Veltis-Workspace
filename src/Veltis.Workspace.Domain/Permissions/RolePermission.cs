using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Permissions;

public sealed class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }
}
