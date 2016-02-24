using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public static void WriteNode(this System.Xml.XmlWriter xw, string node,
			Dictionary<string, string> attributes)
		{
			xw.WriteStartElement(node);

			foreach (var it in attributes)
				xw.WriteAttributeString(it.Key, it.Value);

			xw.WriteEndElement();

			xw.Flush();
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
	}
}
