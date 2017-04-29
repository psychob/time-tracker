using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Tracking.Mouse
{
	class MouseWheelEvent: MouseEvent
	{
		public readonly int Value;

		public MouseWheelEvent(string type, int x, int y, int value, int button = 0 )
			: base(type, x, y, button)
		{
			Value = value;
		}

		public override int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			int written = base.AsByteStream(ref outputStream, start, length);
			throw new NotImplementedException();
		}
	}
}
