using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;
using timetracker.BasePlugin.Messages;

namespace timetracker
{
	public partial class TrackSystem
	{
		// interface
		internal interface TokenValue
		{
			int AsByteStream(ref byte[] str, int start, int length);
		}

		FileStream StreamBinary;

		void AppendBinary(TokenValue value, DateTime? dt = null, bool ForceFlush = false)
		{
		}
	}
}
