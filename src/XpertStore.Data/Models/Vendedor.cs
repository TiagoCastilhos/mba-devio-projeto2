namespace XpertStore.Data.Models;

public class Vendedor
{
    public Guid Id { get; set; }

    public ICollection<Produto> Produtos { get; set; }
}
