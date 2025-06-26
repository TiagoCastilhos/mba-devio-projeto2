using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Models.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DevXpert.Store.Core.Business.Services
{
    public class AuthService(UserManager<IdentityUser> userManager,
                             SignInManager<IdentityUser> signInManager,
                             IClienteService clienteService,
                             IOptions<JWTSettings> jwtSettings) : IAuthService
    {
        //private readonly JWTSettings _jwtSettings = jwtSettings.Value;

        public async Task<AuthResultViewModel> RegisterAsync(UserRegisterViewModel usuarioRegistro)
        {
            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, usuarioRegistro.Password);

            if (!result.Succeeded)
            {
                return new AuthResultViewModel
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            await userManager.AddToRoleAsync(user, "Cliente");

            var cliente = new Cliente(
                Guid.Parse(user.Id),
                user.UserName,
                user.Email,
                usuarioRegistro.Password);

            if (!await clienteService.Adicionar(cliente))
            {
                await userManager.DeleteAsync(user);
                return new AuthResultViewModel
                {
                    Success = false,
                    Errors = ["Falha ao cadastrar cliente."]
                };
            }

            await clienteService.Salvar();
            await signInManager.SignInAsync(user, false);

            var token = await GerarJwt(user.Email);

            return new AuthResultViewModel
            {
                Success = true,
                Token = token
            };
        }

        public async Task<AuthResultViewModel> LoginAsync(UserLoginViewModel login)
        {
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);

            if (!result.Succeeded)
            {
                return new AuthResultViewModel
                {
                    Success = false,
                    Errors = ["Usuário ou senha incorretos."]
                };
            }

            var cliente = await clienteService.BuscarPorEmail(login.Email);

            if (cliente is null)
            {
                return new AuthResultViewModel
                {
                    Success = false,
                    Errors = ["Este usuário não é um cliente."]
                };
            }

            var token = await GerarJwt(login.Email);

            return new AuthResultViewModel
            {
                Success = true,
                Token = token
            };
        }

        private async Task<string> GerarJwt(string email)
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

            return tokenHandler.WriteToken(token);
        }

        private async Task<List<Claim>> GetUserClaims(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
                claims.Add(new(ClaimTypes.Role, role));

            return claims;
        }
    }
}