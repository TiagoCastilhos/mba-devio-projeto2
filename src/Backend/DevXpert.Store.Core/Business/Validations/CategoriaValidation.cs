using DevXpert.Store.Core.Business.Models;
using FluentValidation;

namespace DevXpert.Store.Core.Business.Validations
{
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        private const int NomeMinLength = 2;
        private const int NomeMaxLength = 100;
        private const int DescricaoMinLength = 2;
        private const int DescricaoMaxLength = 500;
        public static string NomeEmptyOrNullErrorMsg => "O campo Nome precisa ser fornecido.";
        public static string NomeLengthErrorMsg => $"O campo Nome precisa ter entre {NomeMinLength} e {NomeMaxLength} caracteres.";
        public static string DescricaoEmptyOrNullErrorMsg => "O campo Descrição precisa ser fornecido.";
        public static string DescricaoLengthErrorMsg => $"O campo Descrição precisa ter entre {DescricaoMinLength} e {DescricaoMaxLength} caracteres.";

        public CategoriaValidation()
        {
            RuleFor(p => p.Nome)
                .NotNull().WithMessage(NomeEmptyOrNullErrorMsg)
                .NotEmpty().WithMessage(NomeEmptyOrNullErrorMsg)
                .Length(NomeMinLength, NomeMaxLength).WithMessage(NomeLengthErrorMsg);

            RuleFor(p => p.Descricao)
                .NotNull().WithMessage(DescricaoEmptyOrNullErrorMsg)
                .NotEmpty().WithMessage(DescricaoEmptyOrNullErrorMsg)
                .Length(DescricaoMinLength, DescricaoMaxLength).WithMessage(DescricaoLengthErrorMsg);
        }
    }
}

