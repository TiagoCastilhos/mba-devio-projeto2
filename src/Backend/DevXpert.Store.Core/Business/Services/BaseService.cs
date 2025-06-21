using DevXpert.Store.Core.Business.Models.Base;
using DevXpert.Store.Core.Business.Services.Notificador;
using FluentValidation.Results;

namespace DevXpert.Store.Core.Business.Services
{
    public abstract class BaseService(INotificador notificador)
    {
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notificar(error.ErrorMessage);
        }

        protected void Notificar(string errorMessage)
        {
            notificador.Handle(new Notificacao(errorMessage));
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
    }
}
