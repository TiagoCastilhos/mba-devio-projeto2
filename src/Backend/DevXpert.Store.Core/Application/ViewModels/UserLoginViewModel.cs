using System.ComponentModel.DataAnnotations;

namespace DevXpert.Store.Core.Application.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 6)]
        public string Password { get; set; }
        public bool IsCliente { get; set; } = true;

        public void SetIsCliente(bool isCliente) => IsCliente = isCliente;
    }
}
