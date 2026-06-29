using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Entities;

public sealed class Profession : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }
    public string Slug { get; set; } = string.Empty;
    public bool Active { get; set; } = true;

    public ICollection<UserProfession> UserProfessions { get; } = new List<UserProfession>();
}
