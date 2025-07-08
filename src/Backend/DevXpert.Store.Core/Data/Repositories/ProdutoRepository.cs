using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                           .Where(p => p.Ativo)
                           .ToListAsync();
        }

        public override async Task<IEnumerable<Produto>> Pesquisar(Expression<Func<Produto,bool>> filtro)
        {
            return await Db.Produtos
                           .Include(p => p.Categoria)
                           .Include(p => p.Vendedor)
                           .AsNoTracking()
                           .Where(filtro)
                           .ToListAsync();
        }

        public override async Task<Produto> BuscarPorId(Guid id)
        {
            return await Db.Produtos
                           .Include(p => p.Categoria)
                           .Include(p => p.Vendedor)
                           .FirstOrDefaultAsync(p => p.Id == id);
        }          
        
        public async Task<bool> ProcessarStatusEmLote(Guid vendedorId, bool ativo)
        {
            var produtosAfetados = await Db.Produtos
                .Where(p => p.VendedorId == vendedorId)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(p => p.Ativo, ativo));

            return produtosAfetados != 0;
        }
    }
}
