using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;

namespace timetracker.Tracking.Keyboard
{
	class KeyReleaseEvent : KeyEvent
	{
		public KeyReleaseEvent(uint vk, uint sc)
			: base(Current.Messages.KeyboardRelease, vk, sc)
		{
		}
	}
}
