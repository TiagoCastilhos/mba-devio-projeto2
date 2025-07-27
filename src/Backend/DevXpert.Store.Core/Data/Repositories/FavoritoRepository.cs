using System.Linq.Expressions;
using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Repositories;

public class FavoritoRepository(AppDbContext context) : Repository<Favorito>(context), IFavoritoRepository
{
    public override async Task<IEnumerable<Favorito>> Pesquisar(Expression<Func<Favorito, bool>> predicate)
    {
        return await Db.Favoritos
                       .Include(f => f.Produto)
                           .ThenInclude(f=> f.Categoria)
                       .Include(f => f.Produto)
                           .ThenInclude(p => p.Vendedor)
                       .AsNoTracking()
                       .Where(predicate)
                       .ToListAsync();
    }

    public async Task<Favorito> BuscarPorClienteProduto(Guid clienteId, Guid produtoId)
    {
        return await Db.Favoritos
                       .FirstOrDefaultAsync(f => f.ClienteId == clienteId &&
                                                 f.ProdutoId == produtoId);
    }

    public async Task<bool> ExcluirLote(Guid produtoId)
    {
        await Db.Favoritos
            .Where(f => f.ProdutoId == produtoId)
            .ExecuteDeleteAsync();

        return true;
    }
}