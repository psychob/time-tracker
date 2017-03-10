using System;

namespace timetracker.Tracking
{
	internal interface IDatabase
	{
		void AppendMessage(IMessage msg);

		void AppendMessage(IMessage msg, DateTime time);

		void SetProperty(string Name, long Value);

		void SetProperty(string Name, ulong Value);

		void SetProperty(string Name, string Value);
	}
}
