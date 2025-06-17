using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace XpertStore.Api.Models;

public class CategoriaViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Preencha o campo {0}!")]
    public string? Nome { get; set; }

    [DisplayName("Descrição")]
    [Required(ErrorMessage = "Preencha o campo {0}!")]
    public string? Descricao { get; set; }
}
