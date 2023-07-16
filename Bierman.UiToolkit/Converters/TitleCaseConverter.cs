using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Bierman.UiToolkit.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class TitleCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string inputString)
            {
                return ConvertToTitleCase(inputString);
            }

            // If the input is not a string or is null, return an empty string or handle it as needed.
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If you need to support two-way binding, you can implement the ConvertBack method here.
            // However, for title case conversion, it is not necessary.
            return value;
        }

        private string ConvertToTitleCase(string input)
        {
            // Implement your ConvertToTitleCase function here or use the existing implementation.
            // For example:
            // TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            // string[] words = input.Split(' ');
            // ... (rest of the ConvertToTitleCase logic)

            // For demonstration purposes, we'll use the existing ConvertToTitleCase function
            // that we created earlier.
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0 || !IsLowercaseWord(words[i]))
                {
                    words[i] = textInfo.ToTitleCase(words[i]);
                }
                else
                {
                    words[i] = words[i].ToLower();
                }
            }

            return string.Join(" ", words);
        }

        private bool IsLowercaseWord(string word)
        {
            // Add the list of lowercase words to be excluded from title case conversion
            string[] lowercaseWords = new string[]
            {
            "a", "an", "the",
            "and", "but", "or", "nor", "for", "so", "yet",
            "in", "on", "at", "from", "to", "with", "by", "of", "over", "under", "among", "between"
            };

            return lowercaseWords.Contains(word.ToLower());
        }
    }
}