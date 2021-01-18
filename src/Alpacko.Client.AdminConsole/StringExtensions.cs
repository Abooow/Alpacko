using System;
using System.Collections.Generic;
using System.Globalization;

namespace Alpacko.Client.AdminConsole
{
    public static class StringExtensions
    {
        public static int ToInt32(this string str, int defaultValue)
        {
            if (int.TryParse(str, out int result))
                defaultValue = result;

            return defaultValue;
        }

        public static int? ToInt32(this string str)
        {
            return int.TryParse(str, out int result) ? result : (int?)null;
        }

        public static string UppercaseFirst(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            str = str.Trim().ToLower();
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}