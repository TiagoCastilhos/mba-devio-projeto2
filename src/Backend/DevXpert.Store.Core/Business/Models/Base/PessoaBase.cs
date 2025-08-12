namespace DevXpert.Store.Core.Business.Models.Base;

public abstract class PessoaBase : BaseEntity
{
    public string Nome { get; set; }
    public string Email { get; set; }
}