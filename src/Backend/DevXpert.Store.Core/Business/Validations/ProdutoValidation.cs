using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Business.Validations.Utils;
using FluentValidation;

namespace DevXpert.Store.Core.Business.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        private const int NomeMinLength = 2;
        private const int NomeMaxLength = 100;
        private const int DescricaoMinLength = 2;
        private const int DescricaoMaxLength = 500;
        private const int EstoqueMinValue = 0;
        private const int ImagemMaxLength = 100;
        private const decimal PrecoMinValue = 0.01m;
        private const decimal PrecoMaxValue = 1000000.0m;

        public static string NomeEmptyOrNullErrorMsg => "O campo Nome precisa ser fornecido.";
        public static string NomeLengthErrorMsg => $"O campo Nome precisa ter entre {NomeMinLength} e {NomeMaxLength} caracteres.";
        public static string DescricaoEmptyOrNullErrorMsg => "O campo Descrição precisa ser fornecido.";
        public static string DescricaoLengthErrorMsg => $"O campo Descrição precisa ter entre {DescricaoMinLength} e {DescricaoMaxLength} caracteres.";
        public static string EstoqueEmptyOrNullErrorMsg => "O campo Estoque precisa ser fornecido.";
        public static string EstoqueMinErrorMsg => $"O campo Estoque precisa ser maior ou igual a {EstoqueMinValue}.";
        public static string CategoriaIdEmptyOrNullErrorMsg => "Selecione a Categoria.";
        public static string PrecoEmptyOrNullErrorMsg => "O campo Preco precisa ser fornecido.";
        public static string PrecoRangeErrorMsg => $"Informe um Preço entre R${PrecoMinValue:C2} e R${PrecoMaxValue:C2}.";
        public static string VendedorIdEmptyOrNullErrorMsg => "Erro ao identificar Vendedor.";
        public static string ImagemLengthErrorMsg => $"O campo Imagem precisa ter no máximo {ImagemMaxLength} caracteres.";
        public static string ImagemErrorMsg => "O campo Imagem preciser ser uma imagem com extensão .png, .jpg ou .jpeg";

        public ProdutoValidation()
        {
            RuleFor(p => p.Nome)
                .NotNull().WithMessage(NomeEmptyOrNullErrorMsg)
                .NotEmpty().WithMessage(NomeEmptyOrNullErrorMsg)
                .Length(NomeMinLength, NomeMaxLength).WithMessage(NomeLengthErrorMsg);

            RuleFor(p => p.Descricao)
                .NotNull().WithMessage(DescricaoEmptyOrNullErrorMsg)
                .NotEmpty().WithMessage(DescricaoEmptyOrNullErrorMsg)
                .Length(DescricaoMinLength, DescricaoMaxLength).WithMessage(DescricaoLengthErrorMsg);

            RuleFor(p => p.Estoque)
                .NotNull().WithMessage(EstoqueEmptyOrNullErrorMsg)
                .NotEmpty().WithMessage(EstoqueEmptyOrNullErrorMsg)
                .GreaterThanOrEqualTo(EstoqueMinValue).WithMessage(EstoqueMinErrorMsg);

            RuleFor(p => p.Preco)
                .NotNull().WithMessage(PrecoEmptyOrNullErrorMsg)
                .NotEmpty().WithMessage(PrecoEmptyOrNullErrorMsg)
                .InclusiveBetween(PrecoMinValue, PrecoMaxValue).WithMessage(PrecoRangeErrorMsg);

            RuleFor(p => p.Imagem)
                .MaximumLength(ImagemMaxLength).WithMessage(ImagemLengthErrorMsg)
                .Must(StringValidations.IsValidMidiaFileExtension).WithMessage(ImagemErrorMsg);

            RuleFor(p => p.CategoriaId)
                .NotNull().WithMessage(CategoriaIdEmptyOrNullErrorMsg)
                .NotEmpty().WithMessage(CategoriaIdEmptyOrNullErrorMsg);

            RuleFor(p => p.VendedorId)
                .NotNull().WithMessage(VendedorIdEmptyOrNullErrorMsg)
                .NotEmpty().WithMessage(VendedorIdEmptyOrNullErrorMsg);
        }
    }
}