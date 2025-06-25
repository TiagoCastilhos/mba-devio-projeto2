using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Cliente> BuscarPorId(Guid id);
        Task<bool> Adicionar(Cliente vendedor);
        Task<bool> Atualizar(Cliente vendedor);
        Task Salvar();
    }
}
