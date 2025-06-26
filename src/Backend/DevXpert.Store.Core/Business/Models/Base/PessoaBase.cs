namespace DevXpert.Store.Core.Business.Models.Base;

public abstract class PessoaBase : BaseEntityAtivavel
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}