using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Repositories
{
    public interface IFavoritoRepository : IRepository<Favorito>
    {
        public Task<Favorito> BuscarPorClienteProduto(Guid clienteId, Guid produtoId);
        public bool ExcluirLote(Guid produtoId);
    }
}