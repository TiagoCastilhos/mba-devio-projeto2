using FluentValidation.Results;

namespace DevXpert.Store.Core.Business.Models.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
