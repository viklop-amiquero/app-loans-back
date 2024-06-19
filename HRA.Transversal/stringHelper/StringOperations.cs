using System.Globalization;
using System.Text;

namespace HRA.Transversal.stringHelper
{
    public class StringOperations
    {
        public static string Normalize(string input)
        {
            return new string(input.Normalize(NormalizationForm.FormD)
                                     .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                                     .ToArray());
        }
    }
}
