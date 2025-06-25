using DevXpert.Store.Core.Business.Models.Base;
using DevXpert.Store.Core.Business.Validations;

namespace DevXpert.Store.Core.Business.Models
{
    public class Cliente : PessoaBase
    {
        #region NAVIGATION PROPERTIES
        public Guid ProdutoId { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
        #endregion

        public Cliente()
        {

        }

        public Cliente(Guid id, string nome, string email, string senha, bool ativo = true)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Ativo = ativo;
        }

        public override bool IsValid()
        {
            ValidationResult = new ClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
