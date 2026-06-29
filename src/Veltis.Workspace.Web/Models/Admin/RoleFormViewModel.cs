using System.ComponentModel.DataAnnotations;

namespace Veltis.Workspace.Web.Models.Admin;

public sealed class RoleFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Informe o nome.")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public bool IsSystemRole { get; set; }
}
