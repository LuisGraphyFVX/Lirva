using System;
using System.Collections.Generic;
using System.Text;

namespace Lirva.Extension
{
    public static class String
    {
        public static string FirstCharacterToUpper(this string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1);
        }
        public static string FirstCharacterToLower(this string str)
        {
            return char.ToLower(str[0]) + str.Substring(1);
        }
        public static string TrimEnd(this string str, string value)
        {
            return str.EndsWith(value) ? str.Remove(str.LastIndexOf(value, StringComparison.Ordinal)) : str;
        }
        public static string ReplaceAt(this string str, string replace, int index, int length = 1)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                    .Insert(index, replace);
        }
    }
}
