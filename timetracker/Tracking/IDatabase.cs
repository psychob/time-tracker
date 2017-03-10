using System;

namespace timetracker.Tracking
{
	internal interface IDatabase
	{
		void AppendMessage(IMessage msg);

		void AppendMessage(IMessage msg, DateTime time);
	}
}
