using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> BuscarTodos(string busca, bool? ativo = null);
        Task<Categoria> BuscarPorId(Guid id);
        Task<bool> Adicionar(Categoria categoria);
        Task<bool> Atualizar(Categoria categoria);
        Task<bool> Excluir(Guid id);
        Task Salvar();
    }
}
