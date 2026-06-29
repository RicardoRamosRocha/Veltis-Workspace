using System.ComponentModel.DataAnnotations;

namespace Veltis.Workspace.Application.Professions;

public sealed class ProfessionInputDto
{
    [Required(ErrorMessage = "Informe o nome.")]
    [StringLength(120, ErrorMessage = "O nome deve ter no maximo 120 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "A descricao deve ter no maximo 500 caracteres.")]
    public string? Description { get; set; }

    [StringLength(80)]
    public string? Icon { get; set; }

    [StringLength(40)]
    public string? Color { get; set; }

    [Required(ErrorMessage = "Informe o slug.")]
    [StringLength(140, ErrorMessage = "O slug deve ter no maximo 140 caracteres.")]
    public string Slug { get; set; } = string.Empty;

    public bool Active { get; set; } = true;
}
