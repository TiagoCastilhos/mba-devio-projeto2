using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XpertStore.Mvc.Models;

public class CategoriaViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Preencha o campo {0}!")]
    public string Nome { get; set; }

    [DisplayName("Descrição")]
    [Required(ErrorMessage = "Preencha o campo {0}!")]
    public string Descricao { get; set; }
}
