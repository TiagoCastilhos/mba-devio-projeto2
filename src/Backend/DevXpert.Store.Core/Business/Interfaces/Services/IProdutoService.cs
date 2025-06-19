using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services
{
    public interface IProdutoService : IDisposable
    {
        Task<IEnumerable<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(Guid id);
        Task<bool> Adicionar(Produto categoria);
        Task<bool> Atualizar(Produto categoria);
        Task<bool> Excluir(Guid id);
        Task Salvar();
    }
}
