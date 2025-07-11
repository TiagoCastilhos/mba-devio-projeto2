using System.Linq.Expressions;
using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Repositories
{
    public class VendedorRepository(AppDbContext context) : Repository<Vendedor>(context), IVendedorRepository
    {
        public override async Task<IEnumerable<Vendedor>> Pesquisar(Expression<Func<Vendedor, bool>> filtro)
        {
            return await Db.Vendedores
                           .Include(c => c.Produto)
                           .AsNoTracking()
                           .Where(filtro)
                           .ToListAsync();
        }

        public override async Task<Vendedor> BuscarPorId(Guid id)
        {
            return await Db.Vendedores
                           .Include(c => c.Produto)
                           .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}