using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using LinqKit;
using System.Linq.Expressions;

namespace DevXpert.Store.Core.Business.Services
{
    public class CategoriaService(ICategoriaRepository categoriaRepository,
                                  IProdutoRepository produtoRepository,
                                  INotificador notificador) : BaseService(notificador), ICategoriaService
    {
        #region READ
        public async Task<IEnumerable<Categoria>> BuscarTodos(string busca, bool? ativo)
        {
            return await categoriaRepository.Pesquisar(MontarFiltro(busca, ativo));
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
        public async Task Salvar()
        {
            await categoriaRepository.Salvar();
        }

        private bool Validate(Categoria categoria, bool isInsert = false)
        {
            if (!IsValid(categoria)) return false;

            var filtro = PredicateBuilder.New<Categoria>(m => m.Nome == categoria.Nome);
            if (!isInsert) filtro = filtro.And(m => m.Id != categoria.Id);

            if (categoriaRepository.Pesquisar(filtro).Result.Any())
                return NotificarError("Categoria já cadastrada.");

            return true;
        }

        private static Expression<Func<Categoria, bool>> MontarFiltro(string buscar, bool? ativo)
        {
            Expression<Func<Categoria, bool>> expression = c => true;

            if (ativo.HasValue)
                expression = expression.And(c => c.Ativo == ativo);

            if (!string.IsNullOrEmpty(buscar))
                expression = expression.And(c => c.Nome.ToUpper().Contains(buscar.ToUpper()) || 
                                                 c.Descricao.ToUpper().Contains(buscar.ToUpper()));

            return expression;
        }
        #endregion
    }
}
