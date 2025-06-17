using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XpertStore.Data.Data;
using XpertStore.Data.Models;
using XpertStore.Mvc.Models;

namespace XpertStore.Mvc.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _context;

    public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrar([Bind("Email,Password,ConfirmPassword")] RegistrarUsuarioViewModel registrarUsuario)
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
            await AddVendedor(user);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(Url.Content("~/"));
        }

        return View(registrarUsuario);
    }

    private async Task AddVendedor(IdentityUser user)
    {
        var userId = await _userManager.GetUserIdAsync(user);
        await _context.Vendedores.AddAsync(new Vendedor { Id = new Guid(userId) });
        await _context.SaveChangesAsync();
    }
}
