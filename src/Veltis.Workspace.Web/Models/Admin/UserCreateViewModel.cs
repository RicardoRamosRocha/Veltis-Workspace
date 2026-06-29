using System.ComponentModel.DataAnnotations;

namespace Veltis.Workspace.Web.Models.Admin;

public sealed class UserCreateViewModel
{
    [Required(ErrorMessage = "Informe o nome.")]
    public string DisplayName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail valido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione uma profissao.")]
    public Guid? ProfessionId { get; set; }

    [Required(ErrorMessage = "Informe a senha.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
    public string Password { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
