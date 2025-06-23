using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services
{
    public interface IProdutoService : IDisposable
    {
        Task<IEnumerable<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(Guid id);
        Task<IEnumerable<Produto>> BuscarPorNome(string nome);
        Task<IEnumerable<Produto>> BuscarPorVendedorId(Guid vendedorId);
        Task<IEnumerable<Produto>> BuscarAtivos();
        Task<bool> Adicionar(Produto categoria);
        Task<bool> Atualizar(Produto categoria);
        Task<bool> Excluir(Guid id);
        Task Salvar();
    }
}