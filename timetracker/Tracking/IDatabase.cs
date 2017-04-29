using System;

namespace timetracker.Tracking
{
	internal interface IDatabase
	{
		void AppendMessage(IMessage msg, bool flush = false, DateTime? now = null);

		void DefineProperty(PropertyType type, string name, PropertyFlags flag, int history = 0);

		void SetProperty(string Name, long Value);

		void SetProperty(string Name, ulong Value);

		void SetProperty(string Name, string Value);
	}

	public enum PropertyType
	{
		UInt, Int, String, Float
	}

	[Flags]
	public enum PropertyFlags
	{
		// no special behaviour
		None = 0,

		// we store history of records
		History = 1,

		// If you display this property we sum all history
		Sum = 2,

		// if you display this property we avg over all history
		Avg = 4,

		// we store records from last interval instead of last x records
		Time = 8,
	}
}
