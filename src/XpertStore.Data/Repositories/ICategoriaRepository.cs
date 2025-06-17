using XpertStore.Data.Models;

namespace XpertStore.Data.Repositories;

public interface ICategoriaRepository
{
    public bool IsNull();
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria> GetByIdAsync(Guid id);
    Task<Categoria> CreateAsync(Categoria entity);
    Task<Categoria> UpdateAsync(Categoria entity);
    Task<Categoria> DeleteAsync(Guid id);
    Task<bool> CategoriaEmUsoAsync(Guid categoriaId);
}
