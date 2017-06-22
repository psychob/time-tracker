using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI
{
	public static class WinUser
	{
		public enum WindowMessage : uint
		{
			/// <summary>
			/// The WM_QUERYENDSESSION message is sent when the user chooses to
			/// end the session or when an application calls one of the system
			/// shutdown functions. If any application returns zero, the session
			/// is not ended. The system stops sending WM_QUERYENDSESSION messages
			/// as soon as one application returns zero.
			/// </summary>
			WM_QUERYENDSESSION = 0x0011,

			/// <summary>
			/// The WM_ENDSESSION message is sent to an application after the
			/// system processes the results of the WM_QUERYENDSESSION message.
			/// The WM_ENDSESSION message informs the application whether the
			/// session is ending.
			/// </summary>
			WM_ENDSESSION = 0x0016,

			/// <summary>
			/// Posted to the window with the keyboard focus when a nonsystem key
			/// is pressed. A nonsystem key is a key that is pressed when the ALT
			/// key is not pressed.
			/// </summary>
			WM_KEYDOWN = 0x0100,

			/// <summary>
			/// Posted to the window with the keyboard focus when a nonsystem key
			/// is released. A nonsystem key is a key that is pressed when the ALT
			/// key is not pressed, or a keyboard key that is pressed when a
			/// window has the keyboard focus.
			/// </summary>
			WM_KEYUP = 0x0101,

			/// <summary>
			/// Posted to the window with the keyboard focus when the user presses
			/// the F10 key (which activates the menu bar) or holds down the ALT
			/// key and then presses another key. It also occurs when no window
			/// currently has the keyboard focus in this case, the WM_SYSKEYDOWN
			/// message is sent to the active window. The window that receives the
			/// message can distinguish between these two contexts by checking the
			/// context code in the lParam parameter.
			/// </summary>
			WM_SYSKEYDOWN = 0x0104,

			/// <summary>
			/// Posted to the window with the keyboard focus when the user
			/// releases a key that was pressed while the ALT key was held down.
			/// It also occurs when no window currently has the keyboard focus
			/// in this case, the WM_SYSKEYUP message is sent to the active window.
			/// The window that receives the message can distinguish between these
			/// two contexts by checking the context code in the lParam parameter.
			/// </summary>
			WM_SYSKEYUP = 0x0105,

			/// <summary>
			/// Posted to a window when the cursor moves. If the mouse is not
			/// captured, the message is posted to the window that contains the
			/// cursor. Otherwise, the message is posted to the window that has
			/// captured the mouse.
			/// </summary>
			WM_MOUSEMOVE = 0x0200,
			WM_MOUSEWHEEL = 0x020A,
			WM_MOUSEHWHEEL = 0x020E,

			WM_LBUTTONDOWN = 0x0201,
			WM_LBUTTONUP = 0x0202,

			WM_RBUTTONDOWN = 0x0204,
			WM_RBUTTONUP = 0x0205,

			WM_MBUTTONDOWN = 0x0207,
			WM_MBUTTONUP = 0x0208,

			WM_XBUTTONDOWN = 0x020B,
			WM_XBUTTONUP = 0x020C,
		}
	}
}