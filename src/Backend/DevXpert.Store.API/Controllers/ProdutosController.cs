using System.Data;
using System.Net;
using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models.Constants;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Store.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProdutosController(IAppIdentityUser user,
                                    INotificador notificador,
                                    IProdutoService produtoService) : MainController(notificador, user)
    {
        #region READ
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] string busca, Guid? vendedorId, Guid? categoriaId)
        {
            var produtos = await produtoService.BuscarTodos(busca, vendedorId, categoriaId);
            var produtosViewModel = ProdutoViewModel.MapToList(produtos);

            if (UserId != Guid.Empty)
            {
                foreach (var produtoViewModel in produtosViewModel)
                {
                    var favoritos = produtos.First(p => p.Id == produtoViewModel.Id).Favoritos;
                    produtoViewModel.FavoritoId = favoritos.FirstOrDefault(f => f.ClienteId == UserId)?.Id;
                }
            }

            return CustomResponse(HttpStatusCode.OK, produtosViewModel);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var produto = await produtoService.BuscarPorId(id);
            var produtoViewModel = ProdutoViewModel.MapToViewModel(produto);

            if (produtoViewModel is not null)
            {
                produtoViewModel.ProdutosVendedor = ProdutoViewModel.MapToList(await produtoService.BuscarTodos(vendedorId: produto.VendedorId));
                return CustomResponse(HttpStatusCode.OK, produtoViewModel);
            }

            NotificarErro("Produto não encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }
        #endregion

        #region WRITE
        [HttpPost]
        [Authorize(Roles = Roles.Vendedor)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            produtoViewModel.SetVendedorId(UserId);

            if (!await produtoService.Adicionar(ProdutoViewModel.MapToEntity(produtoViewModel)))
                return CustomResponse(HttpStatusCode.BadRequest);

            await Salvar(produtoViewModel.Id);

            return CustomResponse(HttpStatusCode.Created, produtoViewModel);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = $"{Roles.Administrator},{Roles.Vendedor}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProdutoViewModel produtoViewModel)
        {
            if (produtoViewModel is null || id != produtoViewModel.Id)
            {
                NotificarErro("O Id informado não é o mesmo passado na query.");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            produtoViewModel.SetVendedorId(UserId);

            if (!await produtoService.Atualizar(ProdutoViewModel.MapToEntity(produtoViewModel)))
                return CustomResponse(HttpStatusCode.BadRequest);

            await Salvar(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = Roles.Vendedor)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await produtoService.Excluir(id))
                return CustomResponse(HttpStatusCode.BadRequest);

            await Salvar(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }
        #endregion

        #region PRIVATE_METHODS
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
            }
        }
        #endregion
    }
}