using DevXpert.Store.Core.Business.Models.Base;
using DevXpert.Store.Core.Business.Validations;
using Microsoft.AspNetCore.Http;

namespace DevXpert.Store.Core.Business.Models
{
    public class Produto : BaseEntityAtivavel
    {
        public int Estoque { get; private set; }
        public decimal Preco { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }        
        public string Imagem { get; private set; }
        public IFormFile FileUpload { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid VendedorId { get; private set; }

        #region NAVIGATION PROPERTIES
        public Categoria Categoria { get; set; }
        public Vendedor Vendedor { get; set; }        
        public IEnumerable<Favorito> Favoritos { get; set; }
        #endregion

        public Produto()
        {

        }

        public Produto(Guid id, int estoque, decimal preco, string nome, string descricao, string imagem,  Guid categoriaId, Guid vendedorId, bool ativo = true, IFormFile fileUpload = null)
        {
            Id = id;
            Estoque = estoque;
            Preco = preco;
            Nome = nome;
            Descricao = descricao;
            Imagem = imagem;
            Ativo = ativo;
            CategoriaId = categoriaId;
            VendedorId = vendedorId;
            FileUpload = fileUpload;
        }

        public override bool IsValid()
        {
            ValidationResult = new ProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
