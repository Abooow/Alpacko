using System;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace Alpacko.API
{
    internal static class Utils
    {
        internal static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString());
                }
                return builder.ToString();
            }
        }
    }

    internal static class Extensions
    {
        public static string UppercaseFirst(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}