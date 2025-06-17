using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace XpertStore.Api.Extensions;

public static class FileExtension
{
    public static async Task<bool> UploadArquivo(this IFormFile arquivo, string imgNome, ModelStateDictionary modelState)
    {
        if (arquivo == null || arquivo.Length == 0)
        {
            modelState.AddModelError(string.Empty, "Forneça uma imagem para este produto!");
            return false;
        }

        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "../XpertStore.Mvc/wwwroot/images");

        if (!Directory.Exists(directoryPath))
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        var filePath = Path.Combine(directoryPath, imgNome);

        if (System.IO.File.Exists(filePath))
        {
            modelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
            return false;
        }

        using var stream = new FileStream(filePath, FileMode.Create);
        await arquivo.CopyToAsync(stream);

        return true;
    }

    public static bool UploadArquivoBase64(this string arquivo, string imgNome, ModelStateDictionary modelState)
    {
        if (string.IsNullOrEmpty(arquivo))
        {
            modelState.AddModelError(string.Empty, "Forneça uma imagem para este produto!");
            return false;
        }

        var imageDataByteArray = Convert.FromBase64String(arquivo);

        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "../XpertStore.Mvc/wwwroot/images");

        if (!Directory.Exists(directoryPath))
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        var filePath = Path.Combine(directoryPath, imgNome);

        if (System.IO.File.Exists(filePath))
        {
            modelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
            return false;
        }

        System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        return true;
    }
}
