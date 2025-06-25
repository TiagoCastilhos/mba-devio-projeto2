using FluentValidation.Results;

namespace DevXpert.Store.Core.Business.Models.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; } = true;

        public ValidationResult ValidationResult { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
