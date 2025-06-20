using System.ComponentModel.DataAnnotations;

namespace DevXpert.Store.Core.Application.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$%*¨'+:&/()=!?@{}\-;<>\]\[_.,^~])[A-Za-z\d#$%*¨'+:&/()=!?@{}\-;<>\]\[_.,^~]{6,}$", ErrorMessage = "O campo {0} precisa ter ao menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }
    }
}
