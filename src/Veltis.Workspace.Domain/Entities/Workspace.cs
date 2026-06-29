using Veltis.Workspace.Domain.Common;
using Veltis.Workspace.Domain.Identity;

namespace Veltis.Workspace.Domain.Entities;

public sealed class Workspace : BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
