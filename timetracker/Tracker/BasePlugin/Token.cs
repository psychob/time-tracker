using System;
using timetracker.BasePlugin;

namespace timetracker.Tracker.BasePlugin
{
	public struct Token
	{
		public DateTime Time;
		public IToken Value;

		public Token(DateTime t, IToken v)
		{
			Time = t;
			Value = v;
		}
	}
}
