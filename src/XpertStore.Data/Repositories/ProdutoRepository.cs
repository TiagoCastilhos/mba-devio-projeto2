using Microsoft.EntityFrameworkCore;
using XpertStore.Data.Data;
using XpertStore.Data.Models;

namespace XpertStore.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ApplicationDbContext _context;

    public ProdutoRepository(ApplicationDbContext context)
    {
        _context = context;

    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async Task<IEnumerable<Produto>> GetProdutosCategoriaVendedorAsync()
    {
        return await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Vendedor)
            .ToListAsync();
    }

    public async Task<Produto> GetByIdAsync(Guid id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        return produto;
    }

    public async Task<Produto> GetProdutoCategoriaVendedorByIdAndUserIdAsync(Guid id)
    {
        var produto = await _context.Produtos
                                        .Include(p => p.Categoria)
                                        .Include(p => p.Vendedor)
                                        .FirstOrDefaultAsync(p => p.Id == id);
        return produto;
    }

    public async Task<Produto> CreateAsync(Produto entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Produto> UpdateAsync(Produto entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Produto?> DeleteAsync(Guid id)
    {
        var produto = await GetByIdAsync(id);

        if (produto == null)
            return null;

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return produto;
    }

    public bool IsNull()
    {
        return _context.Produtos is null;
    }

    public bool Exists(Guid id)
    {
        return (_context.Produtos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
