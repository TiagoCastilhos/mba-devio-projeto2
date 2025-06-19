using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Repositories
{
    public class ProdutoRepository(AppDbContext context) : Repository<Produto>(context), IProdutoRepository
    {
        public override async Task<IEnumerable<Produto>> BuscarTodos()
        {
            return await Db.Produtos
                           .Include(p => p.Categoria)
                           .Include(p => p.Vendedor)
                           .AsNoTracking()
                           .ToListAsync();
        }

        public override async Task<Produto> BuscarPorId(Guid id)
        {
            return await Db.Produtos
                           .Include(p => p.Categoria)
                           .Include(p => p.Vendedor)
                           .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> BuscarPorCategoriaId(Guid categoriaId)
        {
            return await Db.Produtos
                           .Include(p => p.Categoria)
                           .Include(p => p.Vendedor)
                           .Where(p => p.CategoriaId == categoriaId)
                           .AsNoTracking()
                           .ToListAsync();
        }
    }
}
