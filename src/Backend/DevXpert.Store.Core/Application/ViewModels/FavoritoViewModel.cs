using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Application.ViewModels;

public class FavoritoViewModel
{
    public Guid Id { get; set; }
    public ProdutoViewModel Produto { get; set; }

    public static FavoritoViewModel MapToViewModel(Favorito favorito)
    {
        if (favorito == null) return null;
        
        return new FavoritoViewModel
        {
            Id = favorito.Id,
            Produto = ProdutoViewModel.MapToViewModel(favorito.Produto)
        };
    }
}