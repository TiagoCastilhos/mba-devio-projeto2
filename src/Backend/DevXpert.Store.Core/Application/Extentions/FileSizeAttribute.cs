using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DevXpert.Store.Core.Application.Extentions
{
    /// <summary>
    /// Valida o tamanho máximo de um arquivo enviado em Mb.
    /// </summary>
    /// <param name="maxSizeInMb"></param>
    public class FileSizeAttribute(int maxSizeInMb) : ValidationAttribute
    {
        private readonly int _maxSizeInMb = maxSizeInMb;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (value is IFormFile formFile &&
                    formFile != null &&
                    formFile.Length > _maxSizeInMb * 1024 * 1024) ?
                        new ValidationResult(GetErrorMessage()) :
                        ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Arquivo deve ter o tamanho máximo de: {_maxSizeInMb}Mb.";
        }
    }
}