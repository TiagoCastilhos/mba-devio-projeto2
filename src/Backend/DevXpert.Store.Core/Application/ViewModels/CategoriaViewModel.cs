using DevXpert.Store.Core.Common.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DevXpert.Store.Core.Application.Mappings;
using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Application.ViewModels
{
    public class CategoriaViewModel : BaseEntityViewModel
    {
        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Informe o campo Nome da Categoria.")]
        [MaxLength(100, ErrorMessage = "O campo Nome da Categoria deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Informe o campo Descrição.")]
        [MaxLength(500, ErrorMessage = "O campo Descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public int QuantidadeProdutos { get; set; }
        
        public static Categoria MapToEntity(CategoriaViewModel categoriaViewModel) 
            => EntityMapping.MapToCategoria(categoriaViewModel);
        
        public static IEnumerable<CategoriaViewModel> MapToList(IEnumerable<Categoria> categorias) => 
            EntityMapping.MapToListCategoriaViewModel(categorias);
        
        public static CategoriaViewModel MapToViewModel(Categoria categoria) => 
            EntityMapping.MapToCategoriaViewModel(categoria);

        public void Ativar() => Ativo = true;
        public void Inativar() => Ativo = false;
    }
}
