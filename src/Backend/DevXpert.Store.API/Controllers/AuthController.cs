using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Models.Settings;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DevXpert.Store.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(INotificador notificador,
                                IAppIdentityUser user,
                                JWTSettings jWTSettings,
                                SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager) : MainController(notificador, user)
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly JWTSettings _jWTSettings = jWTSettings;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel login)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);

            if (!result.Succeeded)
                return CustomResponse(HttpStatusCode.BadRequest, "Usuário ou senha incorretos.");

            return CustomResponse(HttpStatusCode.OK, await GerarJWT(login.Email));
        }

        private async Task<string> GerarJWT(string email)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var claims = await GetUserClaims(email);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jWTSettings.ExpiracaoTokenMinutos),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jWTSettings.ObterChaveEmBytes()),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        private async Task<List<Claim>> GetUserClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Iss, _jWTSettings.Emissor),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new(ClaimTypes.Role, role));

            foreach (var audience in _jWTSettings.ValidoEm)
                claims.Add(new(JwtRegisteredClaimNames.Aud, audience));

            return claims;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - DateTime.UnixEpoch).TotalSeconds);
    }
}
