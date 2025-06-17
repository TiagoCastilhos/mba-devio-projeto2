using Microsoft.EntityFrameworkCore;
using XpertStore.Data.Data;
using XpertStore.Data.Models;

namespace XpertStore.Data.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ApplicationDbContext _context;

    public CategoriaRepository(ApplicationDbContext context)
    {
        _context = context;

    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<Categoria> GetByIdAsync(Guid id)
    {
        Categoria? categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        return categoria;
    }

    public async Task<Categoria> CreateAsync(Categoria entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Categoria> UpdateAsync(Categoria entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Categoria> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return null;

        _context.Categorias.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> CategoriaEmUsoAsync(Guid categoriaId)
    {
        return await _context.Produtos.AnyAsync(p => p.Categoria.Id == categoriaId);
    }

    public bool IsNull()
    {
        return _context.Categorias is null;
    }
}
