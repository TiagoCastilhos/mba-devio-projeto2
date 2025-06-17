using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiFuncional.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using XpertStore.Api.Controllers.Base;
using XpertStore.Api.Models;
using XpertStore.Data.Data;
using XpertStore.Data.Models;

namespace XpertStore.Api.Controllers;

[AllowAnonymous]
public class AuthController : BaseApiController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly ApplicationDbContext _context;

    public AuthController(SignInManager<IdentityUser> signInManager,
                          UserManager<IdentityUser> userManager,
                          IOptions<JwtSettings> jwtSettings,
                          ApplicationDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _context = context;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar(RegisterUserViewModel registrarUsuario)
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
            return Ok(await GerarJwt(registrarUsuario.Email));
        }

        return Problem("Falha ao registrar o usuário");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserViewModel loginUser)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        if (result.Succeeded)
        {
            return Ok(await GerarJwt(loginUser.Email));
        }

        return Problem("Usuário ou senha incorreta");
    }

    private async Task<string> GerarJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtSettings.Emissor,
            Audience = _jwtSettings.Audiencia,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        return encodedToken;
    }

    private async Task AddVendedor(IdentityUser user)
    {
        var userId = await _userManager.GetUserIdAsync(user);
        await _context.Vendedores.AddAsync(new Vendedor { Id = new Guid(userId) });
        await _context.SaveChangesAsync();
    }
}
