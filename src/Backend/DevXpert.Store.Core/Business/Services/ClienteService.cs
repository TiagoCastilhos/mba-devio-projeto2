using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using LinqKit;

namespace DevXpert.Store.Core.Business.Services
{
    public class ClienteService(IClienteRepository clienteRepository,
                                  INotificador notificador) : BaseService(notificador), IClienteService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        #region READ
        public async Task<Cliente> BuscarPorId(Guid id)
        {
            return await _clienteRepository.BuscarPorId(id);
        }

        public async Task<Cliente> BuscarPorEmail(string email)
        {
            var cliente = await _clienteRepository.Pesquisar(c => c.Email == email && c.Ativo);
            
            return cliente.FirstOrDefault();
        }
        #endregion

        #region WRITE
        public async Task<bool> Adicionar(Cliente cliente)
        {
            if (!Validate(cliente, true)) return false;

            await _clienteRepository.Adicionar(cliente);

            return true;
        }

        public async Task<bool> Atualizar(Cliente cliente)
        {
            if (!Validate(cliente, true)) return false;

            await _clienteRepository.Atualizar(cliente);

            return true;
        }
        #endregion

        #region METHODS
        public async Task Salvar()
        {
            await _clienteRepository.Salvar();
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        private bool Validate(Cliente cliente, bool isInsert = false)
        {
            if (!IsValid(cliente)) return false;

            var expression = PredicateBuilder.New<Cliente>(m => m.Nome == cliente.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != cliente.Id);

            if (_clienteRepository.Pesquisar(expression).Result.Any())
                return NotificarError("Cliente já cadastrado.");

            return true;
        }      
        #endregion
    }
}

