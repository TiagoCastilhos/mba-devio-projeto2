using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.Mappings;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Services.Notificador;
using DevXpert.Store.MVC.Helpers.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DevXpert.Store.MVC.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("vendedores")]
    public class VendedoresController(IVendedorService vendedorService,
                                      IProdutoService produtoService,
                                      ICategoriaService categoriaService,
                                      ICategoriaHelperService categoriaHelperService,
                                      INotificador notificador,
                                      IAppIdentityUser user) : MainController(notificador, user)
    {
        public async Task<IActionResult> Index(string busca, bool? ativo, Guid? categoria)
        {
            ViewBag.FiltroStatus = GetAtivosFilter(ativo);

            ViewBag.FiltroCategoria = await categoriaHelperService.GetCategoriasFilterAsync(categoria);

            var vendedores = VendedorViewModel.MapToList(await vendedorService.BuscarTodos(busca, ativo));
            return View(vendedores);
        }

        [HttpGet("detalhes/{id:Guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            return await GetById(id);
        }

        [HttpGet("editar")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return await GetById(id);
        }

        [HttpPost("editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Email,Id,Ativo,Senha")] VendedorViewModel vendedorViewModel)
        {
            if (id != vendedorViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(vendedorViewModel);

            if (!await vendedorService.AlternarStatus(vendedorViewModel.Id))
            {
                GetErrorsFromNotificador();

                return View(vendedorViewModel);
            }

            await vendedorService.Salvar();

            TempData["Sucesso"] = "Vendedor atualizado.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("alterar-status/{id:guid}")]
        public async Task<IActionResult> AlternarStatusVendedor(Guid id)
        {
            return await GetById(id, "Toggle");
        }

        [HttpPost("alterar-status/{id:guid}"), ActionName("AlternarStatusVendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlternarStatusVendedorConfirmed(Guid id)
        {
            if (!await vendedorService.AlternarStatus(id))
                GetErrorsFromNotificador();

            await vendedorService.Salvar();

            TempData["Sucesso"] = "Status do vendedor atualizado.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("produtos-vendedor/{id:guid}")]
        public async Task<IActionResult> ProdutosVendedor(Guid id, string busca, bool? ativo, Guid? categoria)
        {
            var vendedorViewModel = VendedorViewModel.MapToViewModel(await vendedorService.BuscarPorId(id));
            ViewBag.VendedorId = vendedorViewModel.Id;
            ViewBag.VendedorEmail = vendedorViewModel.Email;

            ViewBag.FiltroStatus = GetAtivosFilter(ativo);
            ViewBag.FiltroCategoria = await categoriaHelperService.GetCategoriasFilterAsync(categoria);

            var produtos = ProdutoViewModel.MapToList(await produtoService.BuscarTodos(busca, id, categoria, ativo));
            return View(produtos);
        }

        [HttpGet("alterar-status-produto/{id:guid}")]
        public async Task<IActionResult> AlternarStatusProduto(Guid id)
        {
            var produto = await produtoService.BuscarPorId(id);
            
            return View("ToggleProduto", ProdutoViewModel.MapToViewModel(produto));
        }

        [HttpPost("alterar-status-produto/{id:guid}"), ActionName("AlternarStatusProduto")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlternarStatusProdutoConfirmed(Guid id, Guid vendedorId)
        {
            if (!await produtoService.AlternarStatus(id))
                GetErrorsFromNotificador();

            await produtoService.Salvar();

            TempData["Sucesso"] = "Status do produto atualizado.";

            return RedirectToAction(nameof(ProdutosVendedor), new { id = vendedorId });
        }      

        #region PRIVATE METHODS

        private async Task<IActionResult> GetById(Guid id, string viewToRedirect = "")
        {
            if (id == Guid.Empty) return NotFound();

            var vendedorViewModel = VendedorViewModel.MapToViewModel(await vendedorService.BuscarPorId(id));

            if (vendedorViewModel is null) return NotFound();

            return View(viewToRedirect, vendedorViewModel);
        }

        #endregion

    }
}
