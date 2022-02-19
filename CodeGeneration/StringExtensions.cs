using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneration
{
    public static class StringExtensions
    {
        public static string FirstLetterToUpper(this string s)
        {
            return $"{s.Substring(0, 1).ToUpper()}{s.Substring(1, s.Length - 1)}";
        }
        public static string FirstLetterToLower(this string s)
        {
            return $"{s.Substring(0, 1).ToLower()}{s.Substring(1, s.Length - 1)}";
        }
    }
}
