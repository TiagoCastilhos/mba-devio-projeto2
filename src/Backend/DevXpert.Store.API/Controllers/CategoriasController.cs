using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.Mappings;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace DevXpert.Store.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CategoriasController(IAppIdentityUser user,
                                      INotificador notificador,
                                      ICategoriaService categoriaService) : MainController(notificador, user)
    {
        private readonly ICategoriaService _categoriaService = categoriaService;

        #region READ
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //TODO: IMPLEMENTAR FILTRO PARA BUSCAR POR PARTE OU TODA DESCRICAO
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _categoriaService.BuscarTodos();
            var lista = MapToList(categorias);

            return CustomResponse(HttpStatusCode.OK, lista);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var categoria = await _categoriaService.BuscarPorId(id);
            var categoriaViewModel = MapToViewModel(categoria);

            if (categoriaViewModel is not null)
                return CustomResponse(HttpStatusCode.OK, categoriaViewModel);

            NotificarErro("Categoria não encontrada.");
            return CustomResponse(HttpStatusCode.NotFound);
        }
        #endregion

        #region WRITE
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _categoriaService.Adicionar(MapToEntity(categoriaViewModel)))
                return CustomResponse(HttpStatusCode.BadRequest);

            await Salvar(categoriaViewModel.Id);

            return CustomResponse(HttpStatusCode.Created, categoriaViewModel);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] CategoriaViewModel categoriaViewModel)
        {
            if (categoriaViewModel is null || id != categoriaViewModel.Id)
            {
                NotificarErro("O Id informado não é o mesmo passado na query.");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _categoriaService.Atualizar(MapToEntity(categoriaViewModel)))
                return CustomResponse(HttpStatusCode.BadRequest);

            await Salvar(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _categoriaService.Excluir(id))
                return CustomResponse(HttpStatusCode.BadRequest);

            await Salvar(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }
        #endregion

        #region PRIVATE_METHODS       
        private static CategoriaViewModel MapToViewModel(Categoria categoria) => EntityMapping.MapToCategoriaViewModel(categoria);

        private static Categoria MapToEntity(CategoriaViewModel categoriaViewModel) => EntityMapping.MapToCategoria(categoriaViewModel);

        private static IEnumerable<CategoriaViewModel> MapToList(IEnumerable<Categoria> categorias) => EntityMapping.MapToListCategoriaViewModel(categorias);

        private async Task Salvar(Guid id)
        {
            try
            {
                await _categoriaService.Salvar();
            }
            catch (DBConcurrencyException)
            {
                if (await _categoriaService.BuscarPorId(id) is not null)
                    throw;

                NotificarErro("Categoria não encontrada.");
                return;
            }
        }
        #endregion

    }
}

