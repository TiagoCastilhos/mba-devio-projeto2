using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models.Settings;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DevXpert.Store.Core.Business.Services
{
    //TODO: EM PROD, SALVAR EM UM Blob Storage / S3 Bucket etc
    public class ArquivoService(INotificador notificador,
                                IWebHostEnvironment environment,
                                IOptions<ArquivoSettings> arquivoSettings,
                                IHttpContextAccessor httpContextAccessor) : BaseService(notificador, httpContextAccessor), IArquivoService
    {
        private readonly ArquivoSettings _arquivoSettings = arquivoSettings.Value;
        private readonly IWebHostEnvironment _environment = environment;

        public bool Excluir(string fileName)
        {
            if (fileName == _arquivoSettings.DefaultImage) return true;

            var filePath = $"{_environment.WebRootPath}{_arquivoSettings.BasePath.Replace("~", string.Empty)}{fileName}";

            if (File.Exists(filePath))
                File.Delete(filePath);

            return true;
        }

        public async Task<bool> Salvar(string fileName, IFormFile file)
        {
            if (fileName == _arquivoSettings.DefaultImage || !string.IsNullOrEmpty(fileName) && file is null)
                return true;

            if (file.Length == 0)
                return NotificarError("Arquivo Corrompido ou vazio.");

            var filePath = $"{_environment.WebRootPath}{_arquivoSettings.BasePath.Replace("~", string.Empty)}{fileName}";

            if (File.Exists(filePath))
                return NotificarError("Já existe um arquivo com este nome.");

            using Stream fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
