using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevXpert.Store.Core.Data.Repositories
{
    public class CategoriaRepository(AppDbContext context) : Repository<Categoria>(context), ICategoriaRepository
    {
        public override async Task<IEnumerable<Categoria>> Pesquisar(Expression<Func<Categoria, bool>> filtro)
        {
            return await Db.Categorias
                           .Include(c => c.Produto)
                           .AsNoTracking()
                           .Where(filtro)
                           .ToListAsync();
        }

        public override async Task<IEnumerable<Categoria>> BuscarTodos()
        {
            return await Db.Categorias
                           .Include(c => c.Produto)
                           .AsNoTracking()
                           .Where(c => c.Ativo)
                           .ToListAsync();
        }

        public override async Task<Categoria> BuscarPorId(Guid id)
        {
            return await Db.Categorias
                           .Include(c => c.Produto)
                           .AsNoTracking()
                           .FirstOrDefaultAsync(c => c.Id == id && c.Ativo);
        }
    }
}
