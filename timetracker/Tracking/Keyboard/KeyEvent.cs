using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Tracking.Keyboard
{
	abstract class KeyEvent : IMessage
	{
		private readonly string type;
		private uint vk;
		private uint sc;

		public KeyEvent(string type, uint vk, uint sc)
		{
			this.type = type;
			this.vk = vk;
			this.sc = sc;
		}

		public int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			throw new NotImplementedException();
		}

		public string GetMessageType()
		{
			return type;
		}
	}
}
