using Microsoft.AspNetCore.Http;

namespace DevXpert.Store.Core.Business.Interfaces.Services
{
    public interface IArquivoService : IDisposable
    {
        Task<bool> Salvar(string fileName, IFormFile file);
        bool Excluir(string fileName);
    }
}

