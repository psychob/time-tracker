using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;

namespace timetracker.Tracking.Keyboard
{
	class KeyPressEvent : KeyEvent
	{
		public KeyPressEvent(uint vk, uint sc)
			: base(Current.Messages.KeyboardPress, vk, sc)
		{
		}
	}
}
