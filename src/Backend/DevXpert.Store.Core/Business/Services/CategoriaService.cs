using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using LinqKit;

namespace DevXpert.Store.Core.Business.Services
{
    public class CategoriaService(ICategoriaRepository categoriaRepository,
                                  IProdutoRepository produtoRepository,
                                  INotificador notificador) : BaseService(notificador), ICategoriaService
    {
        #region READ
      
        public async Task<IEnumerable<Categoria>> BuscarTodos()
        {
            return await categoriaRepository.BuscarTodos();
        }        

        public async Task<Categoria> BuscarPorId(Guid id)
        {
            return await categoriaRepository.BuscarPorId(id);
        }
        #endregion

        #region WRITE
        public async Task<bool> Adicionar(Categoria categoria)
        {
            if (!Validate(categoria, true)) return false;

            await categoriaRepository.Adicionar(categoria);

            return true;
        }

        public async Task<bool> Atualizar(Categoria categoria)
        {
            if (!Validate(categoria)) return false;

            await categoriaRepository.Atualizar(categoria);

            return true;
        }

        public async Task<bool> Excluir(Guid id)
        {
            var categoria = await categoriaRepository.BuscarPorId(id);

            if (categoria is null) return NotificarError("Categoria não encontrada.");

            if (produtoRepository.Pesquisar(p => p.CategoriaId == categoria.Id).Result.Any())
                return NotificarError("Não é possível excluir uma Categoria vinculada a produtos.");

            await categoriaRepository.Excluir(id);

            return true;
        }
        #endregion

        #region METHODS
        public void Dispose()
        {
            categoriaRepository?.Dispose();
            produtoRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Salvar()
        {
            await categoriaRepository.Salvar();
        }

        private bool Validate(Categoria categoria, bool isInsert = false)
        {
            if (!IsValid(categoria)) return false;

            var expression = PredicateBuilder.New<Categoria>(m => m.Nome == categoria.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != categoria.Id);

            if (categoriaRepository.Pesquisar(expression).Result.Any())
                return NotificarError("Categoria já cadastrada.");

            return true;
        }
        #endregion
    }
}
