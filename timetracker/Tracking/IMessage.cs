using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Tracking
{
	interface IMessage
	{
		string GetMessageType();

		int AsByteStream(ref byte[] outputStream, int start, int length);
	}
}
