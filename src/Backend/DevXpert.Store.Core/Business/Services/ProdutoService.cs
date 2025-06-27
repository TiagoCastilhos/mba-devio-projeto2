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
                                IArquivoService arquivoService) : BaseService(notificador), IProdutoService
    {
        #region READ
        public async Task<IEnumerable<Produto>> BuscarTodos(string busca, bool? ativo)
        {
            return await produtoRepository.Pesquisar(MontarFiltro(busca, ativo));
        }

        public async Task<Produto> BuscarPorId(Guid id)
        {
            return await produtoRepository.BuscarPorId(id);
        }

        public async Task<IEnumerable<Produto>> BuscarPorVendedorId(Guid vendedorId)
        {
            return await produtoRepository.BuscarPorVendedorId(vendedorId);
        }
        #endregion

        #region WRITE
        public async Task<bool> Adicionar(Produto produto)
        {
            if (!Validate(produto, true)) return false;

            if (await ManipularImagem(produto, true)) return false;

            await produtoRepository.Adicionar(produto);

            return true;
        }

        public async Task<bool> Atualizar(Produto produto)
        {
            if (!Validate(produto)) return false;

            if (await ManipularImagem(produto, false)) return false;

            await produtoRepository.Atualizar(produto);

            return true;
        }

        public async Task<bool> Excluir(Guid id)
        {
            var produto = await produtoRepository.BuscarPorId(id);

            if (produto is null) return NotificarError("Produto não encontrado.");

            arquivoService.Excluir(produto.Imagem);

            //TODO: EXCLUIR OS FAVORITOS DOS CLIENTES

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

            var filtro = PredicateBuilder.New<Produto>(m => m.Nome == produto.Nome);
            if (!isInsert) filtro = filtro.And(m => m.Id != produto.Id);

            if (produtoRepository.Pesquisar(filtro).Result.Any())
                return NotificarError("Produto já cadastrado.");

            return true;
        }

        private async Task<bool> ManipularImagem(Produto produto, bool isInsert = true)
        {
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

        private static Expression<Func<Produto, bool>> MontarFiltro(string buscar, bool? ativo)
        {
            Expression<Func<Produto, bool>> filtro = c => true;

            if (ativo.HasValue)
                filtro = filtro.And(p => p.Ativo == ativo);

            if (!string.IsNullOrEmpty(buscar))
                filtro = filtro.And(p => p.Nome.Contains(buscar) || p.Descricao.Contains(buscar));

            return filtro;
        }
        #endregion
    }
}