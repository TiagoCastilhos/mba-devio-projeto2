﻿using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.Mappings;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Services.Notificador;
using DevXpert.Store.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevXpert.Store.MVC.Controllers
{
    [Authorize(Roles = $"{Roles.Administrator},{Roles.Vendedor}")]
    [Route("produtos")]
    public class ProdutosController(IProdutoService produtoService,
                                    ICategoriaService categoriaService,
                                    INotificador notificador,
                                    IAppIdentityUser user) : MainController(notificador, user)
    {
        
        public async Task<IActionResult> Index(string busca, bool? ativo)
        {
            ViewBag.FiltroStatus = GetAtivosFilter(ativo);

            var produtos = await produtoService.BuscarTodos(busca, UserId, null, ativo);

            return View(ProdutoViewModel.MapToList(produtos));
        }

        [Route("novo")]
        [Authorize(Roles = Roles.Vendedor)]
        public async Task<IActionResult> Create()
        {
            await CarregarCategorias();

            return View(new ProdutoViewModel());
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCategorias();
                return View(produtoViewModel);
            }

            produtoViewModel.SetVendedorId(UserId);
            produtoViewModel.SetImageProperties(produtoViewModel.Imagem);

            if (!await produtoService.Adicionar(ProdutoViewModel.MapToEntity(produtoViewModel)))
            {
                GetErrorsFromNotificador();

                await CarregarCategorias();
                return View(produtoViewModel);
            }

            await produtoService.Salvar();

            TempData["Sucesso"] = "Produto cadastrado!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("detalhes/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await produtoService.BuscarPorId(id);

            if (produto == null || produto.VendedorId != UserId)
                return NotFound();

            return View(ProdutoViewModel.MapToViewModel(produto));
        }

        [HttpGet("editar/{id:guid}")]
        [Authorize(Roles = $"{Roles.Administrator},{Roles.Vendedor}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await produtoService.BuscarPorId(id);

            if (produto == null || produto.VendedorId != UserId)
                return NotFound();

            var viewModel = ProdutoViewModel.MapToViewModel(produto);
            await CarregarCategorias();

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
                await CarregarCategorias();
                return View(produtoViewModel);
            }

            produtoViewModel.SetVendedorId(UserId);

            produtoViewModel.SetImageProperties(produtoViewModel.Imagem);

            if (!await produtoService.Atualizar(ProdutoViewModel.MapToEntity(produtoViewModel)))
            {
                GetErrorsFromNotificador();

                await CarregarCategorias();
                return View(produtoViewModel);
            }

            await produtoService.Salvar();

            TempData["Sucesso"] = "Produto atualizado!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        [Authorize(Roles = Roles.Vendedor)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await produtoService.BuscarPorId(id);

            if (produto == null || produto.VendedorId != UserId)
                return NotFound();

            return View(ProdutoViewModel.MapToViewModel(produto));
        }

        [HttpPost("excluir/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await produtoService.BuscarPorId(id);

            if (produto == null || produto.VendedorId != UserId)
                return NotFound();

            if (!await produtoService.Excluir(id))
            {
                GetErrorsFromNotificador();
                return View(ProdutoViewModel.MapToViewModel(produto));
            }

            await produtoService.Salvar();

            TempData["Sucesso"] = "Produto Excluído!";
            return RedirectToAction(nameof(Index));
        }

        #region #region PRIVATE METHODS
        private async Task<IEnumerable<CategoriaViewModel>> BuscarCategorias()
        {
            var categorias = await categoriaService.BuscarTodos(string.Empty, true);
            return categorias.Select(EntityMapping.MapToCategoriaViewModel);
        }

        private async Task CarregarCategorias()
        {
            var categorias = await BuscarCategorias();

            ViewBag.Categorias = categorias.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nome
            });

        }
        #endregion
    }
}
