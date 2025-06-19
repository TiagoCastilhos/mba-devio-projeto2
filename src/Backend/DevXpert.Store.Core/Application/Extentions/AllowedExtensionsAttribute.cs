using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DevXpert.Store.Core.Application.Extentions
{
    /// <summary>
    /// Valida as extensões permitidas para o arquivo enviado.
    /// </summary>
    /// <param name="extensions"></param>
    public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
    {
        private readonly string[] _extensions = extensions;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile formFile && formFile != null)
            {
                string extension = Path.GetExtension(formFile.FileName);

                if (!_extensions.Contains(extension.ToLower()))
                    return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            string text = string.Empty;
            string[] extensions = _extensions;

            foreach (string ext in extensions)
                text = $"{text}{ext}/";

            return "O arquivo devem ter uma das extensões: " + text[..^1];
        }
    }
}
