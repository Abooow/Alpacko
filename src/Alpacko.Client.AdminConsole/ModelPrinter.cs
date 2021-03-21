using Alpacko.Client.AdminConsole.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alpacko.Client.AdminConsole
{
    public static class ModelPrinter
    {
        public static void Print<T>(T model, int indentationSize = 0)
        {
            Print(model, indentationSize, false);
        }

        public static void PrintWithSkip<T>(T model, int indentationSize = 0)
        {
            Print(model, indentationSize, true);
        }

        private static void Print<T>(T model, int indentationSize, bool useSkip)
        {
            string indentation = new string(' ', indentationSize);

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (useSkip && property.CustomAttributes.Any(x => x.AttributeType == typeof(SkipAttribute)))
                    continue;

                string value = property.GetValue(model)?.ToString() ?? "null";
                Console.WriteLine($"{indentation}{property.Name}: {value}");
            }
        }
    }
}