using System.Globalization;
using System.Text;

namespace Ekol_Plast_MVC.Helpers
{
    public static class UrlHelper
    {
        public static string SanitizeUrl(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return string.Empty;

            // Transliterate Cyrillic to Latin
            title = TransliterateToLatin(title);

            // Convert to lowercase
            var sanitized = title.ToLower();

            // Replace spaces with dashes
            sanitized = sanitized.Replace(' ', '-');

            // Remove invalid characters, allowing only alphanumeric and dashes
            sanitized = System.Text.RegularExpressions.Regex.Replace(sanitized, @"[^a-z0-9\-]", "");

            // Collapse multiple dashes into a single dash
            sanitized = System.Text.RegularExpressions.Regex.Replace(sanitized, @"\-{2,}", "-");

            // Trim leading and trailing dashes
            sanitized = sanitized.Trim('-');

            return sanitized;
        }

        private static string TransliterateToLatin(string input)
        {
            var transliterationMap = new Dictionary<char, string>
            {
                // Add common Cyrillic-to-Latin mappings
                { 'А', "A" }, { 'Б', "B" }, { 'В', "V" }, { 'Г', "G" }, { 'Д', "D" },
                { 'Е', "E" }, { 'Ж', "Zh" }, { 'З', "Z" }, { 'И', "I" }, { 'Ј', "J" },
                { 'К', "K" }, { 'Л', "L" }, { 'М', "M" }, { 'Н', "N" }, { 'О', "O" },
                { 'П', "P" }, { 'Р', "R" }, { 'С', "S" }, { 'Т', "T" }, { 'У', "U" },
                { 'Ф', "F" }, { 'Х', "Kh" }, { 'Ц', "Ts" }, { 'Ч', "Ch" }, { 'Ш', "Sh" },
                { 'Щ', "Sht" }, { 'Ъ', "A" }, { 'Ы', "Y" }, { 'Ь', "" }, { 'Э', "E" },
                { 'Ю', "Yu" }, { 'Я', "Ya" }, { 'а', "a" }, { 'б', "b" }, { 'в', "v" },
                { 'г', "g" }, { 'д', "d" }, { 'е', "e" }, { 'ж', "zh" }, { 'з', "z" },
                { 'и', "i" }, { 'ј', "j" }, { 'к', "k" }, { 'л', "l" }, { 'м', "m" },
                { 'н', "n" }, { 'о', "o" }, { 'п', "p" }, { 'р', "r" }, { 'с', "s" },
                { 'т', "t" }, { 'у', "u" }, { 'ф', "f" }, { 'х', "kh" }, { 'ц', "ts" },
                { 'ч', "ch" }, { 'ш', "sh" }, { 'щ', "sht" }, { 'ъ', "a" }, { 'ы', "y" },
                { 'ь', "" }, { 'э', "e" }, { 'ю', "yu" }, { 'я', "ya" }
            };

            var sb = new StringBuilder(input.Length);
            foreach (var c in input)
            {
                if (transliterationMap.TryGetValue(c, out var latinEquivalent))
                {
                    sb.Append(latinEquivalent);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
