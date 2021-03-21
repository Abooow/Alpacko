using System;
using System.Collections.Generic;

namespace Alpacko.Client.AdminConsole
{
    public static class Utils
    {
        public const ConsoleColor ErrorColor = ConsoleColor.Red;
        public const ConsoleColor SuccessColor = ConsoleColor.Green;

        public static void WriteLineInColor(string str, ConsoleColor color)
        {
            ConsoleColor lastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ForegroundColor = lastColor;
        }

        public static void WriteInColor(string str, ConsoleColor color)
        {
            ConsoleColor lastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = lastColor;
        }
    }
}