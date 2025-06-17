using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XpertStore.Api.Models;

public class ProdutoViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Preencha o campo {0}!")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Preencha o campo {0}!")]
    [DisplayName("Descrição")]
    public string Descricao { get; set; }
    public string Imagem { get; set; }

    [NotMapped]
    [DisplayName("Imagem do Produto")]
    public string ImagemUpload { get; set; }

    [DisplayName("Preço")]
    [Required(ErrorMessage = "Preencha o campo {0}!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}.")]
    public decimal Preco { get; set; }


    [Required(ErrorMessage = "Preencha o campo {0}!")]
    [Range(0, int.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}.")]
    public int Estoque { get; set; }

    [Required(ErrorMessage = "Preencha o campo {0}!")]
    public Guid CategoriaId { get; set; }
    public CategoriaViewModel? Categoria { get; set; }
}
