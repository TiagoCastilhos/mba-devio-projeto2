using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;

namespace DevXpert.Store.Core.Data.Repositories
{
    public class CategoriaRepository(AppDbContext context) : Repository<Categoria>(context), ICategoriaRepository
    {       
    }
}
