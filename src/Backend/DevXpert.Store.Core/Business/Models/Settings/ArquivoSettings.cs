namespace DevXpert.Store.Core.Business.Models.Settings
{
    public class ArquivoSettings
    {
        public const string ConfigName = "ArquivoSettings";
        public string BasePath { get; set; }
        public string RelativePath { get; set; }
        public string DefaultImage { get; set; }
    }
}
