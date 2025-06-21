using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using LinqKit;

namespace DevXpert.Store.Core.Business.Services
{
    public class ProdutoService(INotificador notificador,
                                IProdutoRepository produtoRepository,                               
                                IArquivoService arquivoService) : BaseService(notificador), IProdutoService
    {
        #region READ
        public async Task<IEnumerable<Produto>> BuscarTodos()
        {
            return await produtoRepository.BuscarTodos();
        }

        public async Task<Produto> BuscarPorId(Guid id)
        {
            return await produtoRepository.BuscarPorId(id);
        }

        public async Task<IEnumerable<Produto>> BuscarPorNome(string nome)
        {
            return await produtoRepository.BuscarPorNome(nome);
        }

        public async Task<IEnumerable<Produto>> BuscarPorVendedorId(Guid vendedorId)
        {
            return await produtoRepository.BuscarPorVendedorId(vendedorId);
        }
        
        public async Task<IEnumerable<Produto>> BuscarAtivos()
        {
            return await produtoRepository.BuscarAtivos();
        }
        #endregion

        #region WRITE
        public async Task<bool> Adicionar(Produto produto)
        {
            if (!Validate(produto, true)) return false;

            await ManipularImagem(produto, true);

            await produtoRepository.Adicionar(produto);

            return true;
        }

        public async Task<bool> Atualizar(Produto produto)
        {
            if (!Validate(produto)) return false;

            await ManipularImagem(produto, false);

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

        public void Dispose()
        {
            produtoRepository?.Dispose();
            GC.SuppressFinalize(this);
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
        #endregion
    }
}