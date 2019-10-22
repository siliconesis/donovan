using System;
using System.Text;

namespace Donovan.Utilities
{
    public static class Encoder
    {
        public static string ToBase64(string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }

        public static string FromBase64(string encodedText)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedText));
        }
    }
}
