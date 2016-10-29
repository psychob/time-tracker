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

		public static WinAPI.NetConnectionStatus ToNet(this int status)
		{
			switch (status)
			{
				case 0:
					return WinAPI.NetConnectionStatus.Disconnected;

				case 1:
					return WinAPI.NetConnectionStatus.Connecting;

				case 2:
					return WinAPI.NetConnectionStatus.Connected;

				case 3:
					return WinAPI.NetConnectionStatus.Disconnecting;

				case 4:
					return WinAPI.NetConnectionStatus.HardwareNotPresent;

				case 5:
					return WinAPI.NetConnectionStatus.HardwareDisabled;

				case 6:
					return WinAPI.NetConnectionStatus.HardwareMalfunction;

				case 7:
					return WinAPI.NetConnectionStatus.MediaDisconnected;

				case 8:
					return WinAPI.NetConnectionStatus.Authenticating;

				case 9:
					return WinAPI.NetConnectionStatus.AuthenticationSucceeded;

				case 10:
					return WinAPI.NetConnectionStatus.AuthenticationFailed;

				case 11:
					return WinAPI.NetConnectionStatus.InvalidAddress;

				case 12:
					return WinAPI.NetConnectionStatus.CredentialsRequired;

				default:
					return WinAPI.NetConnectionStatus.Other;
			}
		}

		public static void Fill<T>(this T[] arr, T value)
		{
			for (var it = 0; it < arr.Length; ++it)
				arr[it] = value;
		}

		public static string Flatten<T>(this T[] arr)
		{
			StringBuilder sb = new StringBuilder();

			foreach (var it in arr)
			{
				sb.Append(it);
				sb.Append(',');
			}

			return sb.ToString();
		}
	}
}
