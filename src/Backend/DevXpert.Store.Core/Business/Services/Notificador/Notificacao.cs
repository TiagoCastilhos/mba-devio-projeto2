namespace DevXpert.Store.Core.Business.Services.Notificador
{
    public class Notificacao(string mensagem)
    {
        public string Mensagem { get; } = mensagem;
    }
}
