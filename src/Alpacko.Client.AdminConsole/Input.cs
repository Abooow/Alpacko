using System;
using System.Collections.Generic;
using System.Text;

namespace Alpacko.Client.AdminConsole
{
    public static class Input
    {
        public static string GetPasswordAsHidden()
        {
            StringBuilder builder = new StringBuilder();

            while (true)
            {
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        return builder.ToString();
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        builder.Clear();
                    }
                    else if (keyInfo.Key == ConsoleKey.Backspace && builder.Length > 0)
                    {
                        builder.Remove(builder.Length - 1, 1);
                    }
                    else
                    {
                        builder.Append(Console.CapsLock ? char.ToUpper(keyInfo.KeyChar) : keyInfo.KeyChar);
                    }
                }
            }
        }

        public static string GetPasswordAsStar()
        {
            StringBuilder builder = new StringBuilder();

            while (true)
            {
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        return builder.ToString();
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - builder.Length, Console.CursorTop);
                        Console.Write(new string(' ', builder.Length));
                        Console.SetCursorPosition(Console.CursorLeft - builder.Length, Console.CursorTop);
                        builder.Clear();
                    }
                    else if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Remove(builder.Length - 1, 1);
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(' ');
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                    }
                    else if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsWhiteSpace(keyInfo.KeyChar) || char.IsPunctuation(keyInfo.KeyChar)
                        || "=?\\<>|£$~§".Contains(keyInfo.KeyChar))
                    {
                        Console.Write('*');
                        builder.Append(Console.CapsLock ? char.ToUpper(keyInfo.KeyChar) : keyInfo.KeyChar);
                    }
                }
            }
        }

        public static string GetText()
        {
            StringBuilder builder = new StringBuilder();

            while (true)
            {
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        return builder.ToString();
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - builder.Length, Console.CursorTop);
                        Console.Write(new string(' ', builder.Length));
                        Console.SetCursorPosition(Console.CursorLeft - builder.Length, Console.CursorTop);
                        builder.Clear();
                    }
                    else if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Remove(builder.Length - 1, 1);
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(' ');
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                    }
                    else if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsWhiteSpace(keyInfo.KeyChar) || char.IsPunctuation(keyInfo.KeyChar)
                        || "=?\\<>|£$~§".Contains(keyInfo.KeyChar))
                    {
                        char chr = Console.CapsLock ? char.ToUpper(keyInfo.KeyChar) : keyInfo.KeyChar;
                        Console.Write(chr);
                        builder.Append(chr);
                    }
                }
            }
        }
    }
}