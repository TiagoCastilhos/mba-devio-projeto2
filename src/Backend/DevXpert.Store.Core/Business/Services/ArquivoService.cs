using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Models.Settings;
using DevXpert.Store.Core.Business.Services.Notificador;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DevXpert.Store.Core.Business.Services
{
    public class ArquivoService(INotificador notificador,
                                IWebHostEnvironment environment,
                                IOptions<ArquivoSettings> arquivoSettings) : BaseService(notificador), IArquivoService
    {
        public bool Excluir(string fileName)
        {
            if (fileName == arquivoSettings.Value.DefaultImage) return true;

            var filePath = $"{environment.WebRootPath}{arquivoSettings.Value.BasePath.Replace("~", string.Empty)}{fileName}";

            if (File.Exists(filePath))
                File.Delete(filePath);

            return true;
        }

        public async Task<bool> Salvar(string fileName, IFormFile file)
        {
            if (file.Length == 0)
                return NotificarError("Arquivo Corrompido ou vazio.");

            var root = Path.Combine(Directory.GetParent(environment.ContentRootPath)!.FullName, arquivoSettings.Value.BasePath);
            var filePath = $"{root}{fileName}";

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
