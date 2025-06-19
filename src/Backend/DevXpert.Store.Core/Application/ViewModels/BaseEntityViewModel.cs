using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DevXpert.Store.Core.Common.ViewModels
{
    public abstract class BaseEntityViewModel
    {
        [Key]
        [ValidateNever]
        public Guid Id { get; set; }

        protected BaseEntityViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
