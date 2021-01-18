using System;
using System.Collections.Generic;

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
    }
}