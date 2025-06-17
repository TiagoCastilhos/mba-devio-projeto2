using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XpertStore.Mvc.Models;

public class RegistrarUsuarioViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    [DisplayName("Senha")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    [DisplayName("Confirmar Senha")]
    public string ConfirmPassword { get; set; }
}
