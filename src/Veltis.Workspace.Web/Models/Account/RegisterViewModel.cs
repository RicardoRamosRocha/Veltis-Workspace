using System.ComponentModel.DataAnnotations;

namespace Veltis.Workspace.Web.Models.Account;

public sealed class RegisterViewModel
{
    [Required(ErrorMessage = "Informe seu nome.")]
    [StringLength(160, ErrorMessage = "O nome deve ter no maximo 160 caracteres.")]
    public string DisplayName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail valido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password), ErrorMessage = "A confirmacao deve ser igual a senha.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;
}
