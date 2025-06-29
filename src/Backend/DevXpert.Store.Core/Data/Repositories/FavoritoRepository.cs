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
            .AsNoTracking()
            .Include(f => f.Produto)
            .Where(predicate)
            .ToListAsync();
    }
}