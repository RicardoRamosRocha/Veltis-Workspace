using System.ComponentModel.DataAnnotations;

namespace Veltis.Workspace.Web.Models.Admin;

public sealed class UserEditViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Informe o nome.")]
    public string DisplayName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail valido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione uma profissao.")]
    public Guid? ProfessionId { get; set; }

    public bool IsActive { get; set; } = true;
}
