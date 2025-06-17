using Microsoft.EntityFrameworkCore;
using XpertStore.Data.Models;

namespace XpertStore.Data.Repositories;

public interface IVendedorRepository
{
    Task<Vendedor> GetByIdAsync(Guid id);
}
