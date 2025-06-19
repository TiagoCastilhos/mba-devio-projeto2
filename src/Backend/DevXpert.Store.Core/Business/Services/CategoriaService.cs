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
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        #region READ
      
        public async Task<IEnumerable<Categoria>> BuscarTodos()
        {
            return await _categoriaRepository.BuscarTodos();
        }        

        public async Task<Categoria> BuscarPorId(Guid id)
        {
            return await _categoriaRepository.BuscarPorId(id);
        }
        #endregion

        #region WRITE
        public async Task<bool> Adicionar(Categoria categoria)
        {
            if (!Validate(categoria, true)) return false;

            await _categoriaRepository.Adicionar(categoria);

            return true;
        }

        public async Task<bool> Atualizar(Categoria categoria)
        {
            if (!Validate(categoria)) return false;

            await _categoriaRepository.Atualizar(categoria);

            return true;
        }

        public async Task<bool> Excluir(Guid id)
        {
            var categoria = await _categoriaRepository.BuscarPorId(id);

            if (categoria is null) return NotificarError("Categoria não encontrada.");

            if (_produtoRepository.Pesquisar(p => p.CategoriaId == categoria.Id).Result.Any())
                return NotificarError("Não é possível excluir uma Categoria vinculada a produtos.");

            await _categoriaRepository.Excluir(id);

            return true;
        }
        #endregion

        #region METHODS
        public void Dispose()
        {
            _categoriaRepository?.Dispose();
            _produtoRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Salvar()
        {
            await _categoriaRepository.Salvar();
        }

        private bool Validate(Categoria categoria, bool isInsert = false)
        {
            if (!IsValid(categoria)) return false;

            var expression = PredicateBuilder.New<Categoria>(m => m.Nome == categoria.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != categoria.Id);

            if (_categoriaRepository.Pesquisar(expression).Result.Any())
                return NotificarError("Categoria já cadastrada.");

            return true;
        }
        #endregion
    }
}
