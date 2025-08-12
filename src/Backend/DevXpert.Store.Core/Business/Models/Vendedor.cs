using DevXpert.Store.Core.Business.Models.Base;
using DevXpert.Store.Core.Business.Validations;

namespace DevXpert.Store.Core.Business.Models
{
    public class Vendedor : PessoaBase
    {
        #region NAVIGATION PROPERTIES
        public Guid ProdutoId { get; set; }
        public IEnumerable<Produto> Produto { get; set; }
        #endregion

        public Vendedor()
        {

        }

        public Vendedor(Guid id, string nome, string email, bool ativo = true)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Ativo = ativo;
        }

        public override bool IsValid()
        {
            ValidationResult = new VendedorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
