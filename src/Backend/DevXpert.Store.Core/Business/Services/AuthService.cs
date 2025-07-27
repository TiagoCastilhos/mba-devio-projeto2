using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Extensions;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Models.Constants;
using DevXpert.Store.Core.Business.Models.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DevXpert.Store.Core.Business.Services
{
    public class AuthService(UserManager<IdentityUser> userManager,
                             SignInManager<IdentityUser> signInManager,
                             IClienteService clienteService,
                             IVendedorService vendedorService,
                             IOptions<JWTSettings> jwtSettings) : IAuthService
    {
        public async Task<AuthResultViewModel> RegisterAsync(UserRegisterViewModel usuarioRegistro, bool gerarToken = false)
        {
            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, usuarioRegistro.Password);

            if (!result.Succeeded)
                return AuthViewModel(false, [.. result.Errors.Select(e => e.Description)]);

            bool registered = usuarioRegistro.IsCliente ? await HandleCliente(user, usuarioRegistro.Password) :
                                                          await HandleVendedor(user, usuarioRegistro.Password);

            if (!registered)
                return AuthViewModel(false, [$"Falha ao cadastrar {(usuarioRegistro.IsCliente ? "cliente" : "vendedor")}."]);

            await signInManager.SignInAsync(user, false);

            return gerarToken ? await GerarJwt(user.Email) : AuthViewModel(true, [], string.Empty);
        }

        public async Task<AuthResultViewModel> LoginAsync(UserLoginViewModel login, bool gerarToken = false)
        {
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);

            if (!result.Succeeded)
                return AuthViewModel(false, ["Usuário ou senha incorretos."]);

            if (!await UsuarioExists(login.Email))
                return AuthViewModel(false, [$"Este usuário não é um {(login.IsCliente ? "cliente" : "vendedor")}."]);

            return gerarToken ? await GerarJwt(login.Email) : AuthViewModel(true, [], string.Empty);
        }

        public async Task<AuthResultViewModel> GerarJwt(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var date = DateTime.Now;
            var claims = await GetUserClaims(email);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = jwtSettings.Value.Emissor,
                Claims = new Dictionary<string, object>
                {
                    { JwtRegisteredClaimNames.Aud, jwtSettings.Value.ValidoEm }
                },
                Expires = date.AddMinutes(jwtSettings.Value.ExpiracaoTokenMinutos),
                NotBefore = date,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Value.Jwt)),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            return AuthViewModel(true, [], tokenHandler.WriteToken(token));
        }

        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null) return [];

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in await userManager.GetRolesAsync(user))
                claims.Add(new(ClaimTypes.Role, role));

            return claims;
        }

        private async Task<bool> UsuarioExists(string email)
        {
            var claims = await GetUserClaims(email);

            if (claims.PossuiRole(Roles.Administrator))
                return true;

            if (claims.PossuiRole(Roles.Cliente))
                return (await clienteService.BuscarPorEmail(email)) is not null;

            if (claims.PossuiRole(Roles.Vendedor))
                return (await vendedorService.BuscarPorEmail(email)) is not null;

            return false;
        }

        private async Task<bool> HandleVendedor(IdentityUser user, string password)
        {
            await userManager.AddToRoleAsync(user, Roles.Vendedor);

            var vendedor = new Vendedor(Guid.Parse(user.Id), user.UserName, user.Email, password);

            if (await vendedorService.Adicionar(vendedor))
            {
                await vendedorService.Salvar();
                return true;
            }

            await userManager.DeleteAsync(user);
            return false;
        }

        private async Task<bool> HandleCliente(IdentityUser user, string password)
        {
            await userManager.AddToRoleAsync(user, Roles.Cliente);

            var cliente = new Cliente(Guid.Parse(user.Id), user.UserName, user.Email, password);

            return await clienteService.Adicionar(cliente);
        }

        private static AuthResultViewModel AuthViewModel(bool success, List<string> errors, string token = "")
        {
            return new AuthResultViewModel()
            {
                Success = success,
                Token = token,
                Errors = errors
            };
        }
    }
}