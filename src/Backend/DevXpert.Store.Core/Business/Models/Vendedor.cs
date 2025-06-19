using DevXpert.Store.Core.Business.Validations;

namespace DevXpert.Store.Core.Business.Models
{
    public class Vendedor : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        #region NAVIGATION PROPERTIES
        public Guid ProdutoId { get; set; }
        public IEnumerable<Produto> Produto { get; set; }
        #endregion

        public Vendedor()
        {

        }

        public Vendedor(Guid id, string nome, string email, string senha, bool ativo = true)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Ativo = ativo;
        }

        public override bool IsValid()
        {
            ValidationResult = new VendedorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
