using System;
using System.Collections.Generic;

namespace Alpacko.Client.AdminConsole
{
    public static class Converter
    {
        public static object ConvertFromString(Type type, string str, out string errorMessage)
        {
            errorMessage = null;

            if (type == typeof(string))
            {
                return str;
            }
            else if (type == typeof(int))
            {
                if (int.TryParse(str, out int result))
                    return result;

                errorMessage = "The input can not be converted into an integer";
                return null;
            }

            throw new Exception("This type is not supported");
        }
    }
}