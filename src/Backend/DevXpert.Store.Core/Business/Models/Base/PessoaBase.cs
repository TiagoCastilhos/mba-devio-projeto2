namespace DevXpert.Store.Core.Business.Models.Base;

public class PessoaBase : BaseEntity
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}