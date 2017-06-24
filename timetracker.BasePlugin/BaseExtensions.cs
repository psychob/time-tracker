using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.BasePlugin
{
    public static class BaseExtensions
    {
        public static void Fill<T>(this T[] array, T value)
        {
            for (int it = 0; it < array.Length; it++)
            {
                array[it] = value;
            }
        }

        public static bool IsEmptyOrNull(this string str)
        {
            return str == null || str == string.Empty;
        }

        public static byte[] GetBytes(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static byte[] GetBytesEncoded(this string str)
        {
            if (str.IsEmptyOrNull())
                return new byte[] { 0, 0, 0, 0 };

            byte[] buff;

            // length of string
            int len = Encoding.UTF8.GetByteCount(str);
            buff = new byte[4 + len];

            BitConverter.GetBytes(len).CopyTo(buff, 0);
            byte[] data = Encoding.UTF8.GetBytes(str);

            data.CopyTo(buff, 4);

            return buff;
        }
    }
}
