using System;

namespace timetracker.Tracking
{
	internal interface IDatabase
	{
		void AppendMessage(IMessage msg, bool flush = false);

		void AppendMessage(IMessage msg, DateTime time, bool flush = false);

		void SetPropertyHistory(string Name, int records = 10, bool enable = true);

		void SetProperty(string Name, long Value);

		void SetProperty(string Name, ulong Value);

		void SetProperty(string Name, string Value);
	}
}
