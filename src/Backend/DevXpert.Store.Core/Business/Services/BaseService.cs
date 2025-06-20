using DevXpert.Store.Core.Business.Constants;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Services.Notificador;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace DevXpert.Store.Core.Business.Services
{
    public abstract class BaseService(INotificador notificador, IHttpContextAccessor httpContextAccessor)
    {
        private readonly INotificador _notificador = notificador;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notificar(error.ErrorMessage);
        }

        protected void Notificar(string errorMessage)
        {
            _notificador.Handle(new Notificacao(errorMessage));
        }

        protected bool IsValid<TE>(TE entity) where TE : BaseEntity
        {
            if (entity.IsValid()) return true;

            Notificar(entity.ValidationResult);
            return false;
        }

        protected bool NotificarError(string errorMessage, bool ret = false)
        {
            Notificar(errorMessage);
            return ret;
        }

        protected TE NotificarError<TE>(string errorMessage)
        {
            Notificar(errorMessage);
            return default;
        }

        protected bool EstaAutenticado()
        {
            return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        }

        protected Guid ObterUsuarioId()
        {
            if (!EstaAutenticado())
                return Guid.Empty;

            var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            return Guid.TryParse(userId, out var id) ? id : Guid.Empty;
        }

        protected bool PossuiRole(string role)
        {
            if (!EstaAutenticado())
                return false;

            return _httpContextAccessor.HttpContext?.User.IsInRole(role) ?? false;
        }

        protected bool UsuarioEAdmin()
        {
            if (!EstaAutenticado())
                return false;

            return _httpContextAccessor.HttpContext?.User.IsInRole(RoleConstants.Administrador) ?? false;
        }
    }
}
