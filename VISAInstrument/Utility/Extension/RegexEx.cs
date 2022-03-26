using System.Text.RegularExpressions;

namespace VISAInstrument.Utility.Extension
{
    public static class RegexEx
    {
        public static bool IsMatch(this string input,string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }
    }
}