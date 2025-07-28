using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Store.MVC.Controllers;

public class AuthController(IAuthService authService,
                            INotificador notificador,
                            IAppIdentityUser user) : MainController(notificador, user)
{
    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrar([Bind("Email,Password,ConfirmPassword")] UserRegisterViewModel registrarUsuario)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        registrarUsuario.SetIsCliente(false);

        var result = await authService.RegisterAsync(registrarUsuario);

        if (result.Success)
            return LocalRedirect(Url.Content("~/"));

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error);
        
        return View(registrarUsuario);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Index", "Home");

        model.SetIsCliente(false);

        var result = await authService.LoginAsync(model);

        if(result.Success)
            return RedirectToAction("Index", "Home");

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error);

        return View(model);
    }    
}
