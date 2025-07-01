using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services;

public interface IFavoritoService
{
    Task<IEnumerable<Favorito>> BuscarPorClienteId(Guid clienteId);
    Task<bool> Adicionar(Favorito favorito);
    Task<bool> Excluir(Guid clienteId, Guid ProdutoId);
    Task Salvar();
}