using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;

namespace DevXpert.Store.Core.Business.Services;

public class FavoritoService(
    IFavoritoRepository favoritoRepository,
    INotificador notificador) : BaseService(notificador), IFavoritoService
{
    #region READ

    public async Task<IEnumerable<Favorito>> BuscarPorClienteId(Guid clienteId)
    {
        return await favoritoRepository.Pesquisar(f => 
            f.ClienteId == clienteId
            && f.Produto.Ativo == true);
    }

    #endregion

    #region WRITE

    public async Task<bool> Adicionar(Favorito favorito)
    {
        if (!await Validate(favorito)) return false;

        await favoritoRepository.Adicionar(favorito);

        return true;
    }

    public async Task<bool> Excluir(Guid id)
    {
        var favorito = await favoritoRepository.BuscarPorId(id);

        if (favorito is null) return NotificarError("Favorito não encontrado.");

        await favoritoRepository.Excluir(id);

        return true;
    }

    #endregion

    #region METHODS

    public async Task Salvar()
    {
        await favoritoRepository.Salvar();
    }

    private async Task<bool> Validate(Favorito favorito)
    {
        if (await Existe(favorito.ClienteId, favorito.ProdutoId))
            return NotificarError("Produto já favoritado.");

        return true;
    }

    private async Task<bool> Existe(Guid clienteId, Guid produtoId)
    {
        return (await favoritoRepository.Pesquisar(f =>
            f.ClienteId == clienteId && f.ProdutoId == produtoId)).Any();
    }

    #endregion
}