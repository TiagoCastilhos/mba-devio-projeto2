using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services;

public interface IFavoritoService
{
    Task<IEnumerable<Favorito>> BuscarPorClienteId(Guid clienteId);
    Task<Favorito> BuscarPorId(Guid id);
    Task<bool> Adicionar(Favorito favorito);
    Task<bool> Excluir(Guid id);
    Task Salvar();
}