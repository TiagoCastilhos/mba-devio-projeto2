using System.Linq.Expressions;
using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using DevXpert.Store.Core.Data.Repositories;
using LinqKit;

namespace DevXpert.Store.Core.Business.Services
{
    public class VendedorService(IVendedorRepository vendedorRepository,
                                  INotificador notificador) : BaseService(notificador), IVendedorService
    {
        #region READ
        public async Task<IEnumerable<Vendedor>> BuscarTodos(string busca, bool? ativo = true)
        {
            return await vendedorRepository.Pesquisar(MontarFiltro(busca, ativo));
        }
        public async Task<Vendedor> BuscarPorId(Guid id)
        {
            return await vendedorRepository.BuscarPorId(id);
        }

        public async Task<Vendedor> BuscarPorEmail(string email)
        {
            var vendedor = await vendedorRepository.Pesquisar(v => v.Email == email && v.Ativo);

            return vendedor.FirstOrDefault();
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
            if (!Validate(vendedor)) return false;

            await vendedorRepository.Atualizar(vendedor);

            return true;
        }
        #endregion

        #region METHODS
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

        private static Expression<Func<Vendedor, bool>> MontarFiltro(string buscar, bool? ativo = true)
        {
            Expression<Func<Vendedor, bool>> expression = c => true;

            if (ativo.HasValue)
                expression = expression.And(p => p.Ativo == ativo);

            if (!string.IsNullOrEmpty(buscar))
                expression = expression.And(c => c.Nome.Contains(buscar) || c.Email.Contains(buscar));

            return expression;
        }
        #endregion
    }
}

