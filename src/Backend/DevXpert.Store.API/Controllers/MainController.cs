using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevXpert.Store.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador notificador;
        protected Guid UserId { get; set; }
        protected string UserName { get; set; }

        protected MainController(INotificador notificador,
                                 IAppIdentityUser user)
        {
            this.notificador = notificador;

            if (user.IsAuthenticated())
            {
                UserId = user.GetUserId();
                UserName = user.GetUsername();
            }
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotificarInvalidModelStateError(modelState);

            return CustomResponse(HttpStatusCode.BadRequest);
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode, object result = null)
        {
            return statusCode switch
            {
                HttpStatusCode.OK => Ok(new { success = true, data = result }),
                HttpStatusCode.Created => CreatedAtAction("GetById", new { success = true, id = GetObjectId(result) }, result),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.NotFound => NotFound(new { success = false, errors = SetErrors(result) }),
                HttpStatusCode.BadRequest => BadRequest(new { success = false, errors = SetErrors(result) }),
                _ => throw new NotImplementedException($"Status code {statusCode} não implementado."),
            };
        }

        protected void NotificarInvalidModelStateError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in errors)
            {
                var errorMsg = erro.Exception is null ? erro.ErrorMessage : erro.Exception.Message;

                NotificarErro(errorMsg);
            }
        }

        protected void NotificarInvalidModelStateError(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);

            NotificarInvalidModelStateError(ModelState);
        }

        protected void NotificarErro(string errorMessage)
        {
            notificador.Handle(new Notificacao(errorMessage));
        }

        private Guid GetObjectId(object result)
        {
            dynamic d = result;
            return (Guid)d.Id;
        }

        private object SetErrors(object result)
        {
            return notificador.TemNotificacao() ? notificador.ObterNotificacoes().Select(n => n.Mensagem) : result;
        }
    }
}

