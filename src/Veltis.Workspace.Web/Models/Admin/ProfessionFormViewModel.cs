using System.ComponentModel.DataAnnotations;

namespace Veltis.Workspace.Web.Models.Admin;

public sealed class ProfessionFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Informe o nome.")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }

    [Required(ErrorMessage = "Informe o slug.")]
    public string Slug { get; set; } = string.Empty;

    public bool Active { get; set; } = true;
}
