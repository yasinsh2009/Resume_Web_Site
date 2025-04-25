using System.Text.RegularExpressions;

namespace Resume.Application.Tools
{
    public static class ValidatorHelper
    {
        public static bool IsEmail(string input)
        {
            return Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
