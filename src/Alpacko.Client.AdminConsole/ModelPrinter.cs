using System;
using System.Collections.Generic;
using System.Reflection;

namespace Alpacko.Client.AdminConsole
{
    public static class ModelPrinter
    {
        public static void Print<T>(T model, int indentationSize = 0)
        {
            string indentation = new string(' ', indentationSize);

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string value = property.GetValue(model)?.ToString() ?? "null";
                Console.WriteLine($"{indentation}{property.Name}: {value}");
            }
        }
    }
}