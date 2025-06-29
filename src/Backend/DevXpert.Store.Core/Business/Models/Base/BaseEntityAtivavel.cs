namespace DevXpert.Store.Core.Business.Models.Base;

public abstract class BaseEntityAtivavel : BaseEntity
{
    public bool Ativo { get; set; } = true;

    public void Ativar() =>
        Ativo = true;

    public void Inativar() =>
        Ativo = false;
}