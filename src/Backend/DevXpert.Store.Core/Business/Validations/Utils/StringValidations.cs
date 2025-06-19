using System.Text.RegularExpressions;

namespace DevXpert.Store.Core.Business.Validations.Utils
{
    public static class StringValidations
    {
        private const string emailRegex = @"^[a-z0-9-._]+@[a-z0-9_-]+?\.[a-z.-]{2,30}$";
        private const string properNameRegex = @"^[a-zà-ÿ]+(\s?[a-zà-ÿ][-'.]?\s?)*([a-zà-ÿ]|[jr.|I|II|III|IV]?)*$";

        public static bool CheckIsValidMail(string mail)
        {
            return new Regex(emailRegex, RegexOptions.IgnoreCase).IsMatch(mail ?? string.Empty);
        }

        public static bool CheckIsValidName(string nome)
        {
            return new Regex(properNameRegex, RegexOptions.IgnoreCase).IsMatch(nome ?? string.Empty);
        }

        public static bool IsValidMidiaFileExtension(string file)
        {
            return string.IsNullOrEmpty(file) || file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("png") || file.ToLower().EndsWith("jpeg");
        }
    }
}
