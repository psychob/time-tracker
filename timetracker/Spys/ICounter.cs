using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Spys
{
	internal abstract class ICounter
	{
		public enum CounterType
		{
			Int,
			UInt,
			Float,
		}

		public enum CounterKind
		{
			Moment,
			Average,
			Alltime,
		}

		public abstract CounterType Type
		{
			get;
		}

		public abstract CounterKind Kind
		{
			get;
		}

		public abstract long GetInt();

		public abstract ulong GetUInt();

		public abstract double GetFloat();

		public abstract IConversionTypes GetConvertion();
	}
}
