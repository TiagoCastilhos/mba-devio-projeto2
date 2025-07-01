using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.Mappings;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevXpert.Store.MVC.Controllers
{
    [Authorize]
    [Route("produtos")]
    public class ProdutosController(IProdutoService produtoService, 
                                    ICategoriaService categoriaService,
                                    INotificador notificador,
                                    IAppIdentityUser user) : MainController(notificador, user)
    {
        private readonly IProdutoService _produtoService = produtoService;
        private readonly ICategoriaService _categoriaService = categoriaService;


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //Se usuario estiver autenticado, mostra apenas os produtos dele
            var produtos = _user.IsAuthenticated()
                ? await _produtoService.BuscarPorVendedorId(UserId)
                : await _produtoService.BuscarTodos();

            //Mapeia as entidades para a ViewModel e envia para a View
            return View(MapToViewModelList(produtos));
        }
        [Route("novo")]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProdutoViewModel
            {
                Categorias = await BuscarCategorias()
            };
            return View(viewModel);
        }


        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) 
            {
                produtoViewModel.Categorias = await BuscarCategorias(); //para recarregar as categorias
                return View(produtoViewModel); 
            }

            //definindo o vendedor
            produtoViewModel.SetVendedorId(UserId);
            //definindo o nome da imagem
            produtoViewModel.SetImageProperties(null); 
            
            if (!await _produtoService.Adicionar(MapToEntity(produtoViewModel)))
            {
                //Se houver falhas, exibe o erro e retorna a view
                GetErrorsFromNotificador();
                produtoViewModel.Categorias = await BuscarCategorias();
                return View(produtoViewModel);
            }

            //Persiste as alterações no banco
            await _produtoService.Salvar();
            TempData["Sucesso"] = "Produto cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));

        }

        [HttpGet("detalhes/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await _produtoService.BuscarPorId(id);

            //verifca existencia do produto e se pertence ao usuario logado
            if(produto == null || produto.VendedorId != UserId)
                return NotFound();

            return View(MapToViewModel(produto));
        }

        [HttpGet("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await _produtoService.BuscarPorId(id);

            //verifca existencia do produto e se pertence ao usuario logado
            if (produto == null || produto.VendedorId != UserId)
                return NotFound();

            var viewModel = MapToViewModel(produto);
            viewModel.Categorias = await BuscarCategorias();

            return View(viewModel);
        }


        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                produtoViewModel.Categorias = await BuscarCategorias();
                return View(produtoViewModel);
            }

            //Define novamente o vendedor e a imagem
            produtoViewModel.SetVendedorId(UserId);
            produtoViewModel.SetImageProperties(null);

            if(!await _produtoService.Atualizar(MapToEntity(produtoViewModel)))
            {
                GetErrorsFromNotificador();
                produtoViewModel.Categorias = await BuscarCategorias();
                return View(produtoViewModel);
            }

            await _produtoService.Salvar();
            TempData["Sucesso"] = "Produto atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await _produtoService.BuscarPorId(id);
            if (produto != null || produto.VendedorId != UserId)
                return NotFound();

            return View(MapToViewModel(produto));
        }

        [HttpPost("excluir/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await _produtoService.BuscarPorId(id);
            if(produto == null || produto.VendedorId != UserId)
                return NotFound();

            if(!await _produtoService.Excluir(id))
            {
                GetErrorsFromNotificador();
                return View(MapToViewModel(produto));
            }

            await _produtoService.Salvar();
            TempData["Sucesso"] = "Produto excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }



        #region #region PRIVATE METHODS
        
        //Retorna a lista de categorias já convertendo para ViewModel
        private async Task<IEnumerable<CategoriaViewModel>> BuscarCategorias()
        {
            var categorias = await _categoriaService.BuscarTodos();
            return categorias.Select(EntityMapping.MapToCategoriaViewModel);
        }

        private static ProdutoViewModel MapToViewModel(Produto produto) =>
            EntityMapping.MapToProdutoViewModel(produto);

        private static IEnumerable<ProdutoViewModel> MapToViewModelList(IEnumerable<Produto> produtos) =>
            EntityMapping.MapToListProdutoViewModel(produtos);

        private static Produto MapToEntity(ProdutoViewModel viewModel) =>
            EntityMapping.MapToProduto(viewModel);
        #endregion
    }
}
