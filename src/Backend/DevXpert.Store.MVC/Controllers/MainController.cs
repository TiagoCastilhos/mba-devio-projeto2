using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevXpert.Store.MVC.Controllers
{
    public abstract class MainController : Controller
    {
        private readonly INotificador _notificador;
        public readonly IAppIdentityUser _user;
        protected Guid UserId { get; set; }
        protected string UserName { get; set; }
        protected string Role { get; set; }

        protected MainController(INotificador notificador, IAppIdentityUser user)
        {
            _notificador = notificador;
            _user = user;

            if (user.IsAuthenticated())
            {
                UserId = user.GetUserId();
                UserName = user.GetUsername();
                Role = user.GetUserRole();
            }
        }

        protected void GetErrorsFromNotificador()
        {
            if (!_notificador.TemNotificacao())
                return;

            foreach (var error in _notificador.ObterNotificacoes())
                if (!ModelState.Values.SelectMany(e => e.Errors).Any(e => e.ErrorMessage == error.Mensagem))
                    ModelState.AddModelError(string.Empty, error.Mensagem);
        }

        protected void NotificarErro(string errorMessage)
        {
            _notificador.Handle(new Notificacao(errorMessage));
        }

        protected List<SelectListItem> GetAtivosFilter(bool? selected)
        {
            var listaStatus = new List<SelectListItem> {
                new() { Value = "", Text = "Todos", Selected = false},
                new() { Value = "True", Text = "Ativo", Selected = false},
                new() { Value = "False", Text = "Inativo", Selected = false}
            };

            foreach (var item in listaStatus)
                if (item.Value == selected.ToString())
                    item.Selected = true;

            return listaStatus;

        }
    }
}
