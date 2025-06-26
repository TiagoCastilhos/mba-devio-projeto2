using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services
{
    public interface IVendedorService
    {
        Task<Vendedor> BuscarPorId(Guid id);
        Task<bool> Adicionar(Vendedor vendedor);
        Task<bool> Atualizar(Vendedor vendedor);
        Task Salvar();
    }
}
