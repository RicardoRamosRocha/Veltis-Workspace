namespace Veltis.Workspace.Infrastructure.Identity;

public sealed class IdentitySeederOptions
{
    public const string SectionName = "Seed:AdminUser";

    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? DisplayName { get; set; }
}
