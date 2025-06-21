using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;
using System.Data.Entity;

namespace DevXpert.Store.Core.Data.Repositories
{
    public class CategoriaRepository(AppDbContext context) : Repository<Categoria>(context), ICategoriaRepository
    {
        public override async Task<IEnumerable<Categoria>> BuscarTodos()
        {
            return await Db.Categorias
                           .Include(c => c.Produto)
                           .ToListAsync();
        }

        public override async Task<Categoria> BuscarPorId(Guid id)
        {
            return await Db.Categorias
                           .Include(c => c.Produto)
                           .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
