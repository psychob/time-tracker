using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Spys
{
	internal abstract class IConversionTypes
	{
		public abstract string[] Names
		{
			get;
		}

		public abstract bool InfiniteNames
		{
			get;
		}

		public abstract int[] Stages
		{
			get;
		}

		public abstract bool[] Trim
		{
			get;
		}

		private string GetName(int i)
		{
			// if we have name, then return it
			if (Names.Length > i)
				return Names[i];

			// if we have infinite names
			if (InfiniteNames)
				return Names[Names.Length - 1];

			// if we don't then null it
			return null;
		}

		private int GetStage(int i)
		{
			// first we check if we had enough place in our Stages array
			if (Stages.Length > i)
				return Stages[i];

			// then we pick last element from array, this will throw exception
			// if we don't have enough elements, but that is fine
			return Stages[Stages.Length - 1];
		}

		private bool GetTrim(int i)
		{
			// first we check if we had enough place in our Stages array
			if (Trim.Length > i)
				return Trim[i];

			// then we pick last element from array, this will throw exception
			// if we don't have enough elements, but that is fine
			return Trim[Trim.Length - 1];
		}

//		private string GetImpl(int it, long value)
//		{
//			var name = GetName(it);
//			var stag = GetStage(it);
//			var trim = GetTrim(it);
//		}

		private string GetImpl<T>(int it, T val)
		{
			return string.Empty;
		}

		public string Get(long value)
		{
			return GetImpl(0, value);
		}

		public string Get(ulong value)
		{
			return GetImpl(0, value);
		}

		public string Get(double value)
		{
			return GetImpl(0, value);
		}
	}

	internal sealed class SiConversionTypes : IConversionTypes
	{
		private readonly string[] _Names = new string[]
		{
			"B", "K", "M", "G", "T", "P", "E", "Z", "Y"
		};

		private readonly int[] _Stages = new int[] { 0, 1000 };
		private readonly bool[] _Trim = new bool[] { false };

		public override bool InfiniteNames
		{
			get
			{
				return false;
			}
		}

		public override string[] Names
		{
			get
			{
				return _Names;
			}
		}

		public override int[] Stages
		{
			get
			{
				return _Stages;
			}
		}

		public override bool[] Trim
		{
			get
			{
				return _Trim;
			}
		}
	}

	internal sealed class IecConversionTypes : IConversionTypes
	{
		private readonly string[] _Names = new string[]
		{
			"B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB"
		};

		private readonly int[] _Stages = new int[] { 0, 1024 };
		private readonly bool[] _Trim = new bool[] { false };

		public override bool InfiniteNames
		{
			get
			{
				return false;
			}
		}

		public override string[] Names
		{
			get
			{
				return _Names;
			}
		}

		public override int[] Stages
		{
			get
			{
				return _Stages;
			}
		}

		public override bool[] Trim
		{
			get
			{
				return _Trim;
			}
		}
	}
}
