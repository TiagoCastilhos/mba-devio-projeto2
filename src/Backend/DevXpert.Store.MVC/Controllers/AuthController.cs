using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Store.MVC.Controllers;

public class AuthController(UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager,
                            IVendedorService vendedorService) : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly IVendedorService _vendedorService = vendedorService;

    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrar([Bind("Email,Password,ConfirmPassword")] UserRegisterViewModel registrarUsuario)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var user = new IdentityUser
        {
            UserName = registrarUsuario.Email,
            Email = registrarUsuario.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registrarUsuario.Password);

        if (result.Succeeded)
        {
            await AddVendedor(user, registrarUsuario);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(Url.Content("~/"));
        }

        return View(registrarUsuario);
    }

    private async Task AddVendedor(IdentityUser user, UserRegisterViewModel registrarUsuario)
    {
        var userId = await _userManager.GetUserIdAsync(user);
        await _vendedorService.Adicionar(new Vendedor { Id = new Guid(userId), Email =  registrarUsuario.Email, Nome = registrarUsuario.Email, Senha = registrarUsuario.Password});
        await _vendedorService.Salvar();
    }
}
