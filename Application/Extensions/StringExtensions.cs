using System.Text.RegularExpressions;

namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static (int, string) SplitFormattedPhone(this string formattedPhone)
        {
            var matchesNumbers = new Regex(@"(\d+)").Matches(formattedPhone);

            if (matchesNumbers.Count != 3)
                throw new ArgumentException($"{nameof(formattedPhone)}: número de telefone inválido.");

            return (Convert.ToInt32(matchesNumbers[0].Value), (matchesNumbers[1].Value + matchesNumbers[2].Value));
        }

        public static bool ValidatePhone(this string formattedPhone)
        {
            return new Regex(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$").IsMatch(formattedPhone);
        }

        public static bool ValidateHomePhone(this string formattedPhone)
        {
            return new Regex(@"^\([1-9]{2}\) [2-8]{4}\-[0-9]{4}$").IsMatch(formattedPhone);
        }

        public static bool ValidateCellPhone(this string formattedPhone)
        {
            return new Regex(@"^\([1-9]{2}\) 9[0-9]{4}\-[0-9]{4}$").IsMatch(formattedPhone);
        }
    }
}