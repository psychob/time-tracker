using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static timetracker.TrackSystem;

namespace timetracker
{
	public static class ExtensionMethods
	{
		public static bool Contains<T>(this List<T> list, Predicate<T> pred)
		{
			foreach (var it in list)
				if (pred(it))
					return true;

			return false;
		}

		public static void Replace<T>(this List<T> list, Predicate<T> pred, T newvalue)
		{
			for (var it = 0; it < list.Count; ++it)
			{
				if (pred(list[it]))
				{
					list[it] = newvalue;
					return;
				}
			}
		}

		public static Dictionary<K, V> Filter<K, V>(this Dictionary<K, V> list, Func<K, V, bool> pred)
		{
			Dictionary<K, V> ret = new Dictionary<K, V>();

			foreach (var it in list)
				if (pred(it.Key, it.Value))
					ret.Add(it);

			return ret;
		}

		public static void Add<K, V>(this Dictionary<K, V> dict, KeyValuePair<K, V> pair)
		{
			dict.Add(pair.Key, pair.Value);
		}

		public static string ToSensibleFormat(this DateTime dt)
		{
			return dt.ToString("HH:mm:ss.ffff dd-MM-yyyy");
		}

		public static bool IsSymbol(this char c)
		{
			const string symbols = "!@#$%^&*()_+|\\=-[]{};':\"<>?,./";

			return char.IsSymbol(c) || symbols.IndexOf(c) != -1;
		}

		public static bool IsUpper(this char c)
		{
			return char.IsUpper(c);
		}

		public static bool IsWhite(this char c)
		{
			return char.IsWhiteSpace(c);
		}

		public static bool IsNumber(this char c)
		{
			return char.IsNumber(c);
		}

		public static bool IsEmptyOrNull(this string str)
		{
			return str == null || str == string.Empty;
		}

		public static void Fill<T>(this T[] arr, T value)
		{
			for (var it = 0; it < arr.Length; ++it)
				arr[it] = value;
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
