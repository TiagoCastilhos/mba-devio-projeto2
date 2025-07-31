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
        private readonly string Root = Path.Combine(Directory.GetParent(environment.ContentRootPath)!.FullName, arquivoSettings.Value.BasePath);

        public bool Excluir(string fileName)
        {
            if (fileName == arquivoSettings.Value.DefaultImage) return true;

            var filePath = $"{Root}{fileName}";

            VerificarBaseImagens();

            if (File.Exists(filePath))
                File.Delete(filePath);

            return true;
        }

        public async Task<bool> Salvar(string fileName, IFormFile file)
        {
            if (file.Length == 0)
                return NotificarError("Arquivo Corrompido ou vazio.");

            VerificarBaseImagens();

            var filePath = $"{Root}{fileName}";

            if (File.Exists(filePath))
                return NotificarError("Já existe um arquivo com este nome.");

            using Stream fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return true;
        }

        private void VerificarBaseImagens()
        {            
            if (!Directory.Exists(Root))
                Directory.CreateDirectory(Root);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
