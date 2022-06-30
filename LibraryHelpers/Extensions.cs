using System;

namespace Library.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the string value to Capitalized string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string FirstCharToUpper(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))   
                throw new ArgumentException("Invalid string is passed");
            var a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}