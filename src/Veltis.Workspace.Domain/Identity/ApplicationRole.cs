using Microsoft.AspNetCore.Identity;

namespace Veltis.Workspace.Domain.Identity;

public sealed class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {
        Id = Guid.NewGuid();
    }

    public string? Description { get; set; }
    public bool IsSystemRole { get; set; }
}
