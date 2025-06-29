using DevXpert.Store.Core.Business.Models.Base;

namespace DevXpert.Store.Core.Business.Models;

public class Favorito : BaseEntity
{
    public Guid ClienteId { get; set; }
    public Guid ProdutoId { get; set; }

    #region NAVIGATION PROPERTIES
    public Cliente Cliente { get; set; }
    public Produto Produto { get; set; }
    #endregion

    public Favorito()
    {

    }

    public Favorito(Guid id, Guid clienteId, Guid produtoId)
    {
        Id = id;
        ClienteId = clienteId;
        ProdutoId = produtoId;
    }

    public Favorito(Guid clienteId, Guid produtoId)
    {
        Id = Guid.NewGuid();
        ClienteId = clienteId;
        ProdutoId = produtoId;
    }
}