using System.Data;
using System.Net;
using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Store.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class FavoritosController(IAppIdentityUser user,
                                 INotificador notificador,
                                 IFavoritoService favoritoService) : MainController(notificador, user)
{
    #region READ

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var favoritos = await favoritoService.BuscarPorClienteId(UserId);

        var viewModels = favoritos.Select(FavoritoViewModel.MapToViewModel);

        return CustomResponse(HttpStatusCode.OK, viewModels);
    }

    #endregion

    #region WRITE

    [HttpPost("{produtoId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(Guid produtoId)
    {
        var favorito = new Favorito(Guid.NewGuid(), UserId, produtoId);

        if (!await favoritoService.Adicionar(favorito))
            return CustomResponse(HttpStatusCode.BadRequest);

        await Salvar(favorito.Id);
        return CustomResponse(HttpStatusCode.Created, favorito);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!await favoritoService.Excluir(id))
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
            await favoritoService.Salvar();
        }
        catch (DBConcurrencyException)
        {
            if (await favoritoService.BuscarPorId(id) is not null)
                throw;

            NotificarErro("Categoria não encontrada.");
        }
    }
    #endregion
}