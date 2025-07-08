using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Business.Services.Notificador;
using DevXpert.Store.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Store.MVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger,
                                INotificador notificador,
                                IAppIdentityUser user) : MainController(notificador, user)
    {
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.ErroCode = id;
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
            }
            else if (id == 404)
            {
                modelErro.ErroCode = id;
                modelErro.Titulo = "Ops! P�gina n�o encontrada.";
                modelErro.Mensagem = "A p�gina que est� procurando n�o existe! <br />Em caso de d�vidas entre em contato com nosso suporte";
            }
            else if (id == 403)
            {
                modelErro.ErroCode = id;
                modelErro.Titulo = "Acesso Negado";
                modelErro.Mensagem = "Voc� n�o tem permiss�o para fazer isto.";
            }
            else
                return StatusCode(500);

            return View("Error", modelErro);
        }
    }
}
