using System.Net;
using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Store.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]    
    public class AuthController(INotificador notificador,
                                IAppIdentityUser user,
                                IAuthService authService) : MainController(notificador, user)
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await authService.RegisterAsync(usuarioRegistro);

            if (result.Success) 
                return CustomResponse(HttpStatusCode.OK, result.Token);
            
            foreach (var error in result.Errors)
                NotificarErro(error);

            return CustomResponse(HttpStatusCode.BadRequest);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel login)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await authService.LoginAsync(login);

            if (result.Success)
                return CustomResponse(HttpStatusCode.OK, result.Token);
            
            foreach (var error in result.Errors)
                NotificarErro(error);

            return CustomResponse(HttpStatusCode.BadRequest);
        }
    }
}