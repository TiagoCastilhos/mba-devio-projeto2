using DevXpert.Store.Core.Business.Validations;

namespace DevXpert.Store.Core.Business.Models
{
    public class Cliente : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        #region NAVIGATION PROPERTIES
       //TODO: MAPEAR LISTA DE FAVORITOS
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
