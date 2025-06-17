namespace XpertStore.Data.Models;

public class Categoria
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }

    public ICollection<Produto> Produtos { get; set; }
}
