using Microsoft.AspNetCore.Identity;
using Veltis.Workspace.Domain.Entities;

namespace Veltis.Workspace.Domain.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        Id = Guid.NewGuid();
    }

    public string? DisplayName { get; set; }
    public Guid? ProfessionId { get; set; }
    public Profession? Profession { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<UserProfession> UserProfessions { get; } = new List<UserProfession>();
    public Veltis.Workspace.Domain.Entities.Workspace? Workspace { get; set; }
}
