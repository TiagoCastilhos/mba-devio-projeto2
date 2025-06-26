using System.Net;
using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Business.Interfaces.Services;
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

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorClienteId(Guid id)
    {
        return CustomResponse(HttpStatusCode.OK,await favoritoService.BuscarPorClienteId(id));
    }

    #endregion
}