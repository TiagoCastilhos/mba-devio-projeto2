using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;

namespace DevXpert.Store.Core.Data.Repositories;

public class FavoritoRepository(AppDbContext context) : Repository<Favorito>(context), IFavoritoRepository;