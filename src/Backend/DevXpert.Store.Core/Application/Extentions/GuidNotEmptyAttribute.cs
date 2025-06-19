using System.ComponentModel.DataAnnotations;

namespace DevXpert.Store.Core.Application.Extentions
{
    /// <summary>
    /// Valida se um Guid é diferente de Guid.Empty
    /// </summary>
    /// <param name="fieldName"></param>
    public class GuidNotEmptyAttribute(string fieldName) : ValidationAttribute
    {
        private readonly string _fieldName = fieldName;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (value != null && value is Guid guid && guid != Guid.Empty) ? ValidationResult.Success : new ValidationResult(GetErrorMessage());
        }

        private string GetErrorMessage()
        {
            return "Informe o campo " + _fieldName;
        }
    }
}