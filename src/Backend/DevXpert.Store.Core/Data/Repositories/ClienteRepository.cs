using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Repositories
{
    public class ClienteRepository(AppDbContext context) : Repository<Cliente>(context), IClienteRepository
    {
        public override async Task<Cliente> BuscarPorId(Guid id)
        {
            return await Db.Clientes                                                      
                           .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}