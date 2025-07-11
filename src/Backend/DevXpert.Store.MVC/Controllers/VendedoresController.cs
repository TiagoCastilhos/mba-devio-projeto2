using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Services.Notificador;
using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.ViewModels;

namespace DevXpert.Store.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("vendedores")]
    public class VendedoresController(IVendedorService vendedorService,
                                      IProdutoService produtoService,
                                      INotificador notificador,
                                      IAppIdentityUser user) : MainController(notificador, user)
    {
        public async Task<IActionResult> Index(string busca, bool? ativo)
        {
            ViewBag.FiltroStatus = GetAtivosFilter(ativo);
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

        [HttpGet("/ProdutosVendedor/{id:guid}")]
        public async Task<IActionResult> ProdutosVendedor(Guid id, string busca, bool? ativo)
        {
            //recarregar vendedor
            var vendedorViewModel = VendedorViewModel.MapToViewModel(await vendedorService.BuscarPorId(id));
            ViewBag.VendedorId = vendedorViewModel.Id;
            ViewBag.VendedorEmail = vendedorViewModel.Email;


            ViewBag.FiltroStatus = GetAtivosFilter(ativo);

            var produtos = ProdutoViewModel.MapToList(await produtoService.BuscarTodos(busca, id, null, ativo));
            return View(produtos);
        }

        [HttpPost("ProdutosVendedor/{id:guid}")]
        public async Task<IActionResult> AlternarStatusProduto(Guid id, Guid vendedorId)
        {
            if (!await produtoService.AlternarStatus(id))
                GetErrorsFromNotificador();

            await produtoService.Salvar();

            TempData["Sucesso"] = "Status do produto atualizado.";

            return RedirectToAction(nameof(ProdutosVendedor), new { id = vendedorId });
        }

        #region PRIVATE METHODS

        private async Task<IActionResult> GetById(Guid id, string view = "")
        {
            if (id == Guid.Empty) return NotFound();

            var vendedorViewModel = VendedorViewModel.MapToViewModel(await vendedorService.BuscarPorId(id));

            if (vendedorViewModel is null) return NotFound();

            return string.IsNullOrEmpty(view) ? View(vendedorViewModel) : View(view, vendedorViewModel);
        }

        #endregion
    }
}
