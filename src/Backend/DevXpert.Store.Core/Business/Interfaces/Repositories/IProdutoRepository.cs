using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<bool> ProcessarStatusEmLote(Guid vendedorId, bool ativo);
    }
}

