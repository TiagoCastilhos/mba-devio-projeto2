using DevXpert.Store.Core.Application.ViewModels;

namespace DevXpert.Store.Core.Business.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResultViewModel> RegisterAsync(UserRegisterViewModel usuarioRegistro);
    Task<AuthResultViewModel> LoginAsync(UserLoginViewModel login);
}
