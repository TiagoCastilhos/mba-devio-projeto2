using System.Text;

namespace DevXpert.Store.Core.Business.Models.Settings
{
    public class JWTSettings
    {
        public const string ConfigName = "JWTSettings";

        public string Emissor { get; set; }
        public int ExpiracaoTokenMinutos { get; set; }
        public int ExpiracaoRefreshTokenMinutos { get; set; }
        public string Jwt { get; set; }
        public string[] ValidoEm { get; set; }

        public byte[] ObterChaveEmBytes() 
            => Encoding.ASCII.GetBytes(Jwt);
    }
}
