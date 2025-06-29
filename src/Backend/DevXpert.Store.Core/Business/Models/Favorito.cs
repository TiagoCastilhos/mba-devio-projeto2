using DevXpert.Store.Core.Business.Models.Base;

namespace DevXpert.Store.Core.Business.Models;

public class Favorito : BaseEntity
{
    public Guid ClienteId { get; set; }
    public Guid ProdutoId { get; set; }

    public Cliente Cliente { get; set; }
    public Produto Produto { get; set; }
}