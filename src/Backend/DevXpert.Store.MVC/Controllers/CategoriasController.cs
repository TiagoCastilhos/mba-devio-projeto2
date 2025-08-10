using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models.Constants;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Store.MVC.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("categorias")]
    public class CategoriasController(ICategoriaService categoriaService,
                                      INotificador notificador,
                                      IAppIdentityUser user) : MainController(notificador, user)
    {
        [HttpGet("")]
        public async Task<IActionResult> Index(string busca, bool? ativo)
        {
            ViewBag.FiltroStatus = GetAtivosFilter(ativo);

            var categorias = CategoriaViewModel.MapToList(await categoriaService.BuscarTodos(busca, ativo));
            return View(categorias);
        }

        [Route("detalhes/{id:Guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            return await GetById(id);
        }

        [Route("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Id")] CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
                return View(categoriaViewModel);

            categoriaViewModel.Ativar();

            if (!await categoriaService.Adicionar(CategoriaViewModel.MapToEntity(categoriaViewModel)))
            {
                GetErrorsFromNotificador();

                return View(categoriaViewModel);
            }

            await categoriaService.Salvar();

            TempData["Sucesso"] = "Categoria Cadastrada.";

            return RedirectToAction(nameof(Index));
        }

        [Route("editar")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return await GetById(id);
        }

        [HttpPost("editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Descricao,Id")] CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(categoriaViewModel);

            categoriaViewModel.Ativar();

            if (!await categoriaService.Atualizar(CategoriaViewModel.MapToEntity(categoriaViewModel)))
            {
                GetErrorsFromNotificador();

                return View(categoriaViewModel);
            }

            await categoriaService.Salvar();

            TempData["Sucesso"] = "Categoria Editada.";

            return RedirectToAction(nameof(Index));
        }

        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await GetById(id);
        }

        [HttpPost("excluir/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoriaViewModel = await GetById(id);

            if (!await categoriaService.Excluir(id))
            {
                GetErrorsFromNotificador();

                return View();
            }

            await categoriaService.Salvar();

            TempData["Sucesso"] = "Categoria Excluída!";

            return RedirectToAction(nameof(Index));
        }

        #region PRIVATE METHODS

        private async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var categoria = await categoriaService.BuscarPorId(id);
            var categoriaViewModel = CategoriaViewModel.MapToViewModel(categoria);

            if (categoriaViewModel is null)
                return NotFound();

            return View(categoriaViewModel);
        }       
        #endregion
    }
}
