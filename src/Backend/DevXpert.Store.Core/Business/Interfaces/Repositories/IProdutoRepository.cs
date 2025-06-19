using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> BuscarPorCategoriaId(Guid categoriaId);
        //Task<IEnumerable<Produto>> BuscarFavoritos(Guid ClienteId);
    }
}

