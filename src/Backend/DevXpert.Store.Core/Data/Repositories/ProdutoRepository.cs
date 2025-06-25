using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DevXpert.Store.Core.Data.Repositories
{
    public class ProdutoRepository(AppDbContext context) : Repository<Produto>(context), IProdutoRepository
    {
        public override async Task<IEnumerable<Produto>> BuscarTodos()
        {
            return await ObterQueryProdutosIncluindoRelacoes()
                           .AsNoTracking()
                           .ToListAsync();
        }

        public override async Task<Produto> BuscarPorId(Guid id)
        {
            return await ObterQueryProdutosIncluindoRelacoes()
                           .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> BuscarPorCategoriaId(Guid categoriaId)
        {
            return await ObterQueryProdutosIncluindoRelacoes()
                           .Where(p => p.CategoriaId == categoriaId)
                           .AsNoTracking()
                           .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> BuscarPorNome(string nome)
        {
            return await ObterQueryProdutosIncluindoRelacoes()
                .Where(p => p.Nome.Contains(nome))
                .AsNoTracking()
                .ToListAsync();       
        }

        public async Task<IEnumerable<Produto>> BuscarPorVendedorId(Guid vendedorId)
        {
            return await ObterQueryProdutosIncluindoRelacoes()
                .Where(p => p.VendedorId == vendedorId)
                .AsNoTracking()
                .ToListAsync();       
        }

        public async Task<IEnumerable<Produto>> BuscarAtivos()
        {
            return await ObterQueryProdutosIncluindoRelacoes()
                .Where(p => p.Ativo)
                .AsNoTracking()
                .ToListAsync();       
        }
        
        private IIncludableQueryable<Produto, Vendedor> ObterQueryProdutosIncluindoRelacoes()
        {
            return Db.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor);
        }
    }
}
