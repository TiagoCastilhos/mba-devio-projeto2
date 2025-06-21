using System.Linq.Expressions;
using DevXpert.Store.Core.Business.Models.Base;

namespace DevXpert.Store.Core.Business.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> BuscarTodos();
        Task<TEntity> BuscarPorId(Guid id);
        Task<int> Adicionar(TEntity entity);
        Task<int> Atualizar(TEntity entity);
        Task<int> Excluir(Guid id);
        Task<int> Salvar();
    }
}