using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Models.Settings;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace DevXpert.Store.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(INotificador notificador,
                               IAppIdentityUser user,
                               IOptions<JWTSettings> jwtSettings,
                               IClienteService clienteService,
                               SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager) : MainController(notificador, user)
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly JWTSettings _jwtSettings = jwtSettings.Value;
        private readonly IClienteService _clienteService = clienteService;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Password);

            if (!result.Succeeded)
            {
                NotificarInvalidModelStateError(result);
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            if (!await _clienteService.Adicionar(new Cliente(Guid.Parse(user.Id), user.UserName, user.Email, usuarioRegistro.Password)))
                return CustomResponse(HttpStatusCode.BadRequest, "Falha ao cadastrar cliente.");

            await _clienteService.Salvar();

            await _signInManager.SignInAsync(user, false);

            return CustomResponse(HttpStatusCode.OK, await GerarJWT(user.Email));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel login)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);
            
            var cliente = await _clienteService.BuscarPorEmail(login.Email);
            
            //TODO: PERMITIR LOGIN DE ADMIN OU VENDEDOR PELA API???

            if (!result.Succeeded || cliente is null)
                return CustomResponse(HttpStatusCode.BadRequest, "Usuário ou senha incorretos.");

            return CustomResponse(HttpStatusCode.OK, await GerarJWT(login.Email));
        }

        private async Task<string> GerarJWT(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var date = DateTime.Now;
            var claims = await GetUserClaims(email);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Emissor,
                Claims = new Dictionary<string, object>
                {
                    { JwtRegisteredClaimNames.Aud, _jwtSettings.ValidoEm }
                },
                Expires = date.AddMinutes(_jwtSettings.ExpiracaoTokenMinutos),
                NotBefore = date,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Jwt)),
                                                                SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);

            //TODO: Criar objeto de retorno para o front angular salvar as informações na session/local storage
        }

        private async Task<List<Claim>> GetUserClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>() {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
                claims.Add(new(ClaimTypes.Role, role));

            return claims;
        }
    }
}
