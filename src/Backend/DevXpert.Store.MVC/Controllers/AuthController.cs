using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Store.MVC.Controllers;

public class AuthController(UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager,
                            IVendedorService vendedorService,
                            IAuthService authService) : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly IVendedorService _vendedorService = vendedorService;
    private readonly IAuthService _authService = authService;

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

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null)
            {
                var userClaims = await _authService.GetUserClaims(model.Email);
                if (userClaims != null && userClaims[2].Value == "Vendedor")
                {
                    var vendedor = await _vendedorService.BuscarPorEmail(model.Email);
                    if (vendedor != null && vendedor.Ativo)
                    {
                        await signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Ativo", "Conta Inativa ou inexistente");
                        return View();
                    }
                }
                else if(userClaims != null && userClaims[2].Value == "Administrator")
                {
                    await signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("Ativo", "Conta inativa ou inexistente");
                return View();
            }
        }
        return RedirectToAction("Index", "Home");
    }

    private async Task AddVendedor(IdentityUser user, UserRegisterViewModel registrarUsuario)
    {
        var userId = await _userManager.GetUserIdAsync(user);
        await _vendedorService.Adicionar(new Vendedor { Id = new Guid(userId), Email =  registrarUsuario.Email, Nome = registrarUsuario.Email, Senha = registrarUsuario.Password});
        await _vendedorService.Salvar();
    }
}
