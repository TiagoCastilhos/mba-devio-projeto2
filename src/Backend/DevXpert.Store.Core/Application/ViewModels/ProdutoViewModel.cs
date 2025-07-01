using DevXpert.Store.Core.Application.Extentions;
using DevXpert.Store.Core.Common.ViewModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DevXpert.Store.Core.Application.Mappings;
using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Application.ViewModels
{
    public class ProdutoViewModel : BaseEntityViewModel
    {
        [DisplayName("Produto")]
        [Required(ErrorMessage = "Informe o campo Nome do Produto.")]
        [MaxLength(100, ErrorMessage = "O campo Nome do Produto deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Informe o campo Descrição.")]
        [MaxLength(500, ErrorMessage = "O campo Descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Informe uma quantidade maior que zero.")]
        [DisplayName("Estoque")]
        [Required(ErrorMessage = "Informe a quantidade em estoque.")]
        public int Estoque { get; set; }

        [DisplayName("Preço")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Informe um Preço maior que zero.")]
        [Required(ErrorMessage = "Informe um Preço.")]
        [Moeda("Preço")]
        public decimal Preco { get; set; }

        [GuidNotEmpty("Categorias")]
        [DisplayName("Categorias")]
        [Required(ErrorMessage = "Selecione uma categoria.")]
        public Guid CategoriaId { get; set; }

        [DisplayName("VendedorId")]
        public Guid VendedorId { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions([".jpg", ".jpeg", ".png"])]
        [FileSize(20)]
        [DisplayName("Selecione uma imagem")]
        public IFormFile FileUpload { get; set; }

        public string Imagem { get; set; }
        public bool Ativo { get; set; } = true;

        public string Categoria { get; set; }

        public IEnumerable<CategoriaViewModel> Categorias { get; set; }

        public void SetVendedorId(Guid id)
        {
            VendedorId = id;
        }

        public void SetImageProperties(string imageName)
        {
            Imagem = FileUpload is not null ? $"{Id}_{FileUpload.FileName}" : !string.IsNullOrEmpty(Imagem) ? Imagem : imageName;
        }
        
        public static ProdutoViewModel MapToViewModel(Produto produto) => 
            EntityMapping.MapToProdutoViewModel(produto);

        public static Produto MapToEntity(ProdutoViewModel produtoViewModel) =>
            EntityMapping.MapToProduto(produtoViewModel);

        public static IEnumerable<ProdutoViewModel> MapToList(IEnumerable<Produto> produtos) =>
            EntityMapping.MapToListProdutoViewModel(produtos);
    }
}