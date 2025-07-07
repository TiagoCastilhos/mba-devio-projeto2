using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Services.Notificador;
using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DevXpert.Store.MVC.Controllers
{
    [Authorize]
    [Route("vendedores")]
    public class VendedoresController(IVendedorService vendedorService,
                                      IProdutoService produtoService,
                                      INotificador notificador,
                                      IAppIdentityUser user) : MainController(notificador, user)
    {
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(string busca)
        {
            var vendedores = VendedorViewModel.MapToList(await vendedorService.BuscarTodos(busca, null));
            return View(vendedores);
        }

        [Authorize(Roles = "Administrator")]
        [Route("detalhes/{id:Guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            return await GetById(id);
        }

        [Authorize(Roles = "Administrator")]
        [Route("editar")]
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

            if (!await vendedorService.Atualizar(VendedorViewModel.MapToEntity(vendedorViewModel)))
            {
                GetErrorsFromNotificador();

                return View(vendedorViewModel);
            }

            await vendedorService.Salvar();

            TempData["Sucesso"] = "Vendedor Editado.";

            var produtos = await produtoService.BuscarTodos(null, vendedorViewModel.Id, null, null);
            var produtosViewModel = ProdutoViewModel.MapToList(produtos);
            foreach (var produtoViewModel in produtosViewModel)
            {
                if (vendedorViewModel.Ativo)
                {
                    produtoViewModel.Ativar();
                }
                else
                {
                    produtoViewModel.Inativar();
                }

                var produto = ProdutoViewModel.MapToEntity(produtoViewModel);
                await produtoService.Atualizar(produto);
                await produtoService.Salvar();
            }

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Administrator")]
        [Route("/produtosvendedor/{id:guid}")]
        public async Task<IActionResult> ProdutosVendedor(Guid id)
        {
            var produtos = ProdutoViewModel.MapToList(await produtoService.BuscarTodos(string.Empty, id, null));
            return View(produtos);
        }

        //[HttpPost]
        //public async Task<IActionResult> Inativar(Guid id)
        //{
            
        //}
        #region PRIVATE METHODS

        private async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var vendedor = await vendedorService.BuscarPorId(id);
            var vendedorViewModel = VendedorViewModel.MapToViewModel(vendedor);

            if (vendedorViewModel is null)
                return NotFound();

            return View(vendedorViewModel);
        }

        #endregion
    }
}
