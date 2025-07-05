using DevXpert.Store.Core.Common.ViewModels;
using System.ComponentModel;
using DevXpert.Store.Core.Application.Mappings;
using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Application.ViewModels
{
    public class VendedorViewModel : BaseEntityViewModel
    {
        [DisplayName("Vendedor")]
        public string Nome { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        public bool Ativo { get; set; }

        public int QuantidadeProdutos { get; set; }

        public string Senha { get; set; }

        public static Vendedor MapToEntity(VendedorViewModel vendedorViewModel) 
            => EntityMapping.MapToVendedor(vendedorViewModel);
        
        public static IEnumerable<VendedorViewModel> MapToList(IEnumerable<Vendedor> vendedores) => 
            EntityMapping.MapToListVendedorViewModel(vendedores);
        
        public static VendedorViewModel MapToViewModel(Vendedor vendedor) => 
            EntityMapping.MapToVendedorViewModel(vendedor);

        public void Ativar() => Ativo = true;
        public void Inativar() => Ativo = false;
    }
}
