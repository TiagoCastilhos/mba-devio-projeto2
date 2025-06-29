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
public class FavoritoController(
    IAppIdentityUser user,
    INotificador notificador,
    IFavoritoService favoritoService) : MainController(notificador, user)
{
    #region READ

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorClienteId()
    {
        var clienteId = user.GetUserId();
        var favoritos = await favoritoService.BuscarPorClienteId(clienteId);

        var viewModels = favoritos.Select(FavoritoViewModel.MapToViewModel);

        return CustomResponse(HttpStatusCode.OK, viewModels);
    }

    #endregion

    #region WRITE

    [HttpPost("{produtoId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Adicionar(Guid produtoId)
    {
        var favorito = new Favorito
        {
            ClienteId = user.GetUserId(),
            ProdutoId = produtoId
        };

        if (!await favoritoService.Adicionar(favorito))
            return CustomResponse(HttpStatusCode.BadRequest);

        await favoritoService.Salvar();
        return CustomResponse(HttpStatusCode.OK, favorito);
    }

    #endregion
}