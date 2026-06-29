namespace Veltis.Workspace.Web.Models.Admin;

public sealed class UserAdminViewModel
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ProfessionName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
