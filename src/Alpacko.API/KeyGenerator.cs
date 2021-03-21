using System;
using System.Security.Cryptography;
using System.Text;

namespace Alpacko.API
{
    internal class KeyGenerator
    {
        public static readonly char[] defaultChars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GetUniqueKey(int size)
        {
            return GetUniqueKey(size, defaultChars);
        }

        public static string GetUniqueKey(int size, char[] chars)
        {
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }
}
