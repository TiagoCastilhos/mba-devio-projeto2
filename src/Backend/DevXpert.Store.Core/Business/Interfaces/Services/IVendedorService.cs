using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services
{
    public interface IVendedorService
    {
        Task<IEnumerable<Vendedor>> BuscarTodos(string busca = "", bool? ativo = true);
        Task<Vendedor> BuscarPorId(Guid id);
        Task<Vendedor> BuscarPorEmail(string email);
        Task<bool> Adicionar(Vendedor vendedor);
        Task<bool> Atualizar(Vendedor vendedor);
        Task<bool> AlternarStatus(Guid id);
        Task Salvar();
    }
}
