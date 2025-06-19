using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DevXpert.Store.Core.Application.Extentions
{
    public class MoedaAttribute(string fieldName) : ValidationAttribute
    {
        private readonly string _fieldName = fieldName;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var moeda = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"{_fieldName} em formato inválido.";
        }
    }

    public class MoedaAttributeAdapter(MoedaAttribute attribute, IStringLocalizer stringLocalizer) : AttributeAdapterBase<MoedaAttribute>(attribute, stringLocalizer)
    {
        public override void AddValidation(ClientModelValidationContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-moeda", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-number", GetErrorMessage(context));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return $"Preço em formato inválido.";
        }
    }

    public class MoedaValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is MoedaAttribute moedaAttribute)
                return new MoedaAttributeAdapter(moedaAttribute, stringLocalizer);

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
