using System.Security.Claims;
using DevXpert.Store.Core.Application.ViewModels;

namespace DevXpert.Store.Core.Business.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResultViewModel> RegisterAsync(UserRegisterViewModel usuarioRegistro, bool gerarToken = false);
    Task<AuthResultViewModel> LoginAsync(UserLoginViewModel login, bool gerarToken = false);
    public Task<List<Claim>> GetUserClaims(string email);
}
