using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Tracking.Mouse
{
	class MouseEvent : IMessage
	{
		public readonly string Type;
		public readonly int X;
		public readonly int Y;
		public readonly int ButtonNumber;

		public MouseEvent(string type, int x, int y, int button = 0)
		{
			Type = type;
			X = x;
			Y = y;
			ButtonNumber = button;
		}

		public virtual int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			throw new NotImplementedException();
		}

		public string GetMessageType()
		{
			return Type;
		}
	}
}
