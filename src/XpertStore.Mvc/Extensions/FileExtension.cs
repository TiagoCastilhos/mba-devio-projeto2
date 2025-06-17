namespace XpertStore.Mvc.Extensions;

public static class FileExtension
{
    public static async Task<bool> UploadArquivo(this IFormFile arquivo, string imgPrefixo, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState)
    {
        if (arquivo.Length <= 0) return false;


        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        if (!Directory.Exists(directoryPath))
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        var path = Path.Combine(directoryPath, imgPrefixo + arquivo.FileName);

        if (System.IO.File.Exists(path))
        {
            ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
            return false;
        }

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        return true;
    }

}
