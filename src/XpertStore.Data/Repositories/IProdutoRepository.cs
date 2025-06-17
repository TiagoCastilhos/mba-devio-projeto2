using XpertStore.Data.Models;

namespace XpertStore.Data.Repositories;

public interface IProdutoRepository
{
    bool IsNull();
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<Produto> GetByIdAsync(Guid id);
    Task<Produto> CreateAsync(Produto entity);
    Task<Produto> UpdateAsync(Produto entity);
    Task<Produto> DeleteAsync(Guid id);
    Task<IEnumerable<Produto>> GetProdutosCategoriaVendedorAsync();
    Task<Produto> GetProdutoCategoriaVendedorByIdAndUserIdAsync(Guid id);
    bool Exists(Guid id);
}