using DevXpert.Store.Core.Business.Models.Base;
using DevXpert.Store.Core.Business.Validations;

namespace DevXpert.Store.Core.Business.Models
{
    public class Categoria : BaseEntityAtivavel
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        #region NAVIGATION PROPERTIES
        public Guid ProdutoId { get; set; }
        public IEnumerable<Produto> Produto { get; set; }
        #endregion

        public Categoria()
        {

        }

        public Categoria(Guid id, string nome, string descricao, bool ativo = true)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
        }

        public override bool IsValid()
        {
            ValidationResult = new CategoriaValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}

