using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using LinqKit;

namespace DevXpert.Store.Core.Business.Services
{
    public class VendedorService(IVendedorRepository vendedorRepository,
                                  INotificador notificador) : BaseService(notificador), IVendedorService
    {
        #region READ
        public async Task<Vendedor> BuscarPorId(Guid id)
        {
            return await vendedorRepository.BuscarPorId(id);
        }
        #endregion

        #region WRITE
        public async Task<bool> Adicionar(Vendedor vendedor)
        {
            if (!Validate(vendedor, true)) return false;

            await vendedorRepository.Adicionar(vendedor);

            return true;
        }

        public async Task<bool> Atualizar(Vendedor vendedor)
        {
            if (!Validate(vendedor, true)) return false;

            await vendedorRepository.Atualizar(vendedor);

            return true;
        }
        #endregion

        #region METHODS
        public void Dispose()
        {
            vendedorRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Salvar()
        {
            await vendedorRepository.Salvar();
        }

        private bool Validate(Vendedor vendedor, bool isInsert = false)
        {
            if (!IsValid(vendedor)) return false;

            var expression = PredicateBuilder.New<Vendedor>(m => m.Nome == vendedor.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != vendedor.Id);

            if (vendedorRepository.Pesquisar(expression).Result.Any())
                return NotificarError("Vendedor já cadastrado.");

            return true;
        }      
        #endregion
    }
}

