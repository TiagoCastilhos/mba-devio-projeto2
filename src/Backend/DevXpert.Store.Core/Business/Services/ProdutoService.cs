using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using LinqKit;
using System.Linq.Expressions;

namespace DevXpert.Store.Core.Business.Services
{
    public class ProdutoService(INotificador notificador,
                                IProdutoRepository produtoRepository,
                                IFavoritoRepository favoritoRepository,
                                IArquivoService arquivoService) : BaseService(notificador), IProdutoService
    {
        #region READ
        public async Task<IEnumerable<Produto>> BuscarTodos(string busca = "", Guid? vendedorId = null, Guid? categoriaId = null, bool? ativo = true)
        {
            return await produtoRepository.Pesquisar(MontarFiltro(busca, vendedorId, categoriaId, ativo));
        }

        public async Task<Produto> BuscarPorId(Guid id)
        {
            return await produtoRepository.BuscarPorId(id);
        }
        #endregion

        #region WRITE
        public async Task<bool> Adicionar(Produto produto)
        {
            if (!Validate(produto, true)) return false;

            if (!await ManipularImagem(produto, true)) return false;

            await produtoRepository.Adicionar(produto);

            return true;
        }

        public async Task<bool> Atualizar(Produto produto)
        {
            if (!Validate(produto)) return false;

            if (!await ManipularImagem(produto, false)) return false;

            await produtoRepository.Atualizar(produto);

            return true;
        }

        public async Task<bool> AlternarStatus(Guid id)
        {
            var produto = await produtoRepository.BuscarPorId(id);

            if (produto is null) return NotificarError("Produto não encontrado.");

            produto.AlternarStatus();

            await produtoRepository.Atualizar(produto);

            return true;
        }

        public async Task<bool> Excluir(Guid id)
        {
            var produto = await produtoRepository.BuscarPorId(id);

            if (produto is null) return NotificarError("Produto não encontrado.");

            arquivoService.Excluir(produto.Imagem);

            await favoritoRepository.ExcluirLote(id);

            await produtoRepository.Excluir(id);

            return true;
        }
        #endregion

        #region METHODS
        public async Task Salvar()
        {
            await produtoRepository.Salvar();
        }

        private bool Validate(Produto produto, bool isInsert = false)
        {
            if (!IsValid(produto)) return false;

            var expression = PredicateBuilder.New<Produto>(m => m.Nome == produto.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != produto.Id);

            if (produtoRepository.Pesquisar(expression).Result.Any())
                return NotificarError("Produto já cadastrado.");

            return true;
        }

        private async Task<bool> ManipularImagem(Produto produto, bool isInsert = true)
        {
            if (produto.FileUpload == null || produto.FileUpload.Length == 0)
                return true;

            if (!await arquivoService.Salvar(produto.Imagem, produto.FileUpload))
                return NotificarError("Erro ao salvar arquivo.");

            if (!isInsert)
            {
                var current = await produtoRepository.BuscarPorId(produto.Id);

                if (current is not null && current.Imagem != produto.Imagem)
                    arquivoService.Excluir(current.Imagem);
            }

            return true;
        }

        private static Expression<Func<Produto, bool>> MontarFiltro(string buscar, Guid? vendedorId, Guid? categoriaId, bool? ativo)
        {
            Expression<Func<Produto, bool>> expression = p => true;

            if (ativo.HasValue)
                expression = expression.And(p => p.Ativo == ativo);

            if (vendedorId.HasValue && vendedorId != Guid.Empty)
                expression = expression.And(p => p.VendedorId == vendedorId);

            if (categoriaId.HasValue && categoriaId != Guid.Empty)
                expression = expression.And(p => p.CategoriaId == categoriaId);

            if (!string.IsNullOrEmpty(buscar))
                expression = expression.And(p => p.Nome.Contains(buscar) || p.Descricao.Contains(buscar));

            return expression;
        }
        #endregion
    }
}