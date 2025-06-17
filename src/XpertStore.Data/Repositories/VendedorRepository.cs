using XpertStore.Data.Data;
using XpertStore.Data.Models;

namespace XpertStore.Data.Repositories;
internal class VendedorRepository : IVendedorRepository
{
    private readonly ApplicationDbContext _context;

    public VendedorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Vendedor?> GetByIdAsync(Guid id)
    {
        var vendedor = await _context.Vendedores.FindAsync(id);
        return vendedor;
    }
}
