using Veltis.Workspace.Domain.Common;
using Veltis.Workspace.Domain.Identity;

namespace Veltis.Workspace.Domain.Entities;

public sealed class UserProfession : BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public Guid ProfessionId { get; set; }
    public Profession? Profession { get; set; }
    public bool IsPrimary { get; set; } = true;
}
