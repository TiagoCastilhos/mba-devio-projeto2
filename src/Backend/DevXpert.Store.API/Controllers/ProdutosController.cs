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
    public class ProdutosController(
        IAppIdentityUser user,
        INotificador notificador,
        IProdutoService produtoService) : MainController(notificador, user)
    {
        #region READ

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //TODO: IMPLEMENTAR FILTRO PARA BUSCAR POR PARTE OU TODA DESCRICAO
        public async Task<IActionResult> GetAll()
        {
            var produtos = await produtoService.BuscarTodos();
            var lista = MapToList(produtos);

            return CustomResponse(HttpStatusCode.OK, lista);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var produto = await produtoService.BuscarPorId(id);
            var produtoViewModel = MapToViewModel(produto);

            if (produtoViewModel is not null)
                return CustomResponse(HttpStatusCode.OK, produtoViewModel);

            NotificarErro("Produto não encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        #endregion

        #region WRITE

        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status201Created)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> Post([FromBody] ProdutoViewModel ProdutoViewModel)
        // {
        //     if (!ModelState.IsValid) return CustomResponse(ModelState);

        //     if (!await _ProdutoService.Adicionar(MapToEntity(ProdutoViewModel)))
        //         return CustomResponse(HttpStatusCode.BadRequest);

        //     await Salvar(ProdutoViewModel.Id);

        //     return CustomResponse(HttpStatusCode.Created, ProdutoViewModel);
        // }

        // [HttpPut("{id:guid}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<IActionResult> Put(Guid id, [FromBody] ProdutoViewModel ProdutoViewModel)
        // {
        //     if (ProdutoViewModel is null || id != ProdutoViewModel.Id)
        //     {
        //         NotificarErro("O Id informado não é o mesmo passado na query.");
        //         return CustomResponse(HttpStatusCode.BadRequest);
        //     }

        //     if (!ModelState.IsValid) return CustomResponse(ModelState);

        //     if (!await _ProdutoService.Atualizar(MapToEntity(ProdutoViewModel)))
        //         return CustomResponse(HttpStatusCode.BadRequest);

        //     await Salvar(id);
        //     return CustomResponse(HttpStatusCode.NoContent);
        // }

        // [HttpDelete("{id:guid}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<IActionResult> Delete(Guid id)
        // {
        //     if (!await _ProdutoService.Excluir(id))
        //         return CustomResponse(HttpStatusCode.BadRequest);

        //     await Salvar(id);
        //     return CustomResponse(HttpStatusCode.NoContent);
        // }

        #endregion

        #region PRIVATE_METHODS

        private static ProdutoViewModel MapToViewModel(Produto produto) => EntityMapping.MapToProdutoViewModel(produto);

        private static Produto MapToEntity(ProdutoViewModel ProdutoViewModel) =>
            EntityMapping.MapToProduto(ProdutoViewModel);

        private static IEnumerable<ProdutoViewModel> MapToList(IEnumerable<Produto> produtos) =>
            EntityMapping.MapToListProdutoViewModel(produtos);

        private async Task Salvar(Guid id)
        {
            try
            {
                await produtoService.Salvar();
            }
            catch (DBConcurrencyException)
            {
                if (await produtoService.BuscarPorId(id) is not null)
                    throw;

                NotificarErro("Produto não encontrada.");
                return;
            }
        }

        #endregion
    }
}