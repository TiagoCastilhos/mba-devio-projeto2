using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services
{
    public interface IClienteService : IDisposable
    {
        Task<Cliente> BuscarPorId(Guid id);
        Task<Cliente> BuscarPorEmail(string email);
        Task<bool> Adicionar(Cliente cliente);
        Task<bool> Atualizar(Cliente cliente);
        Task Salvar();
    }
}
