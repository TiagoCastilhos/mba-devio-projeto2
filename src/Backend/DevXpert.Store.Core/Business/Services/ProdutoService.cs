using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using LinqKit;
using Microsoft.AspNetCore.Http;

namespace DevXpert.Store.Core.Business.Services
{
    public class ProdutoService(INotificador notificador,
                                IProdutoRepository produtoRepository,
                                IArquivoService arquivoService,
                                IHttpContextAccessor httpContextAccessor) : BaseService(notificador, httpContextAccessor), IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IArquivoService _arquivoService = arquivoService;

        #region READ
        public async Task<IEnumerable<Produto>> BuscarTodos()
        {
            return await _produtoRepository.BuscarTodos();
        }

        public async Task<Produto> BuscarPorId(Guid id)
        {
            return await _produtoRepository.BuscarPorId(id);
        }
        #endregion

        #region WRITE
        public async Task<bool> Adicionar(Produto produto)
        {
            if (!Validate(produto, true)) return false;

            await ManipularImagem(produto, true);

            await _produtoRepository.Adicionar(produto);

            return true;
        }

        public async Task<bool> Atualizar(Produto produto)
        {
            if (!Validate(produto)) return false;

            await ManipularImagem(produto, false);

            await _produtoRepository.Atualizar(produto);

            return true;
        }

        public async Task<bool> Excluir(Guid id)
        {
            var produto = await _produtoRepository.BuscarPorId(id);

            if (produto is null) return NotificarError("Produto não encontrado.");

            _arquivoService.Excluir(produto.Imagem);

            //TODO: EXCLUIR OS FAVORITOS DOS CLIENTES

            await _produtoRepository.Excluir(id);

            return true;
        }
        #endregion

        #region METHODS
        public async Task Salvar()
        {
            await _produtoRepository.Salvar();
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        private bool Validate(Produto produto, bool isInsert = false)
        {
            if (!IsValid(produto)) return false;

            var expression = PredicateBuilder.New<Produto>(m => m.Nome == produto.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != produto.Id);

            if (_produtoRepository.Pesquisar(expression).Result.Any())
                return NotificarError("Produto já cadastrado.");

            return true;
        }

        private async Task<bool> ManipularImagem(Produto produto, bool isInsert = true)
        {
            if (!await _arquivoService.Salvar(produto.Imagem, produto.FileUpload))
                return NotificarError("Erro ao salvar arquivo.");

            if (!isInsert)
            {
                var current = await _produtoRepository.BuscarPorId(produto.Id);

                if (current is not null && current.Imagem != produto.Imagem)
                    _arquivoService.Excluir(current.Imagem);
            }

            return true;
        }
        #endregion
    }
}