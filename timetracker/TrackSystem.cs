using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace timetracker
{
	public class TrackSystem
	{
		const string ApplicationDefinitions = "appdefinition3.xml";
		const string TrackedTimesCatalogue = "tracks3";

		public static class Structs
		{
			public struct App
			{
				public string Name, UniqueID;
				public ulong Time, StartCounter;
				public bool IsShell;

				public AppRuleSet[] Rules;

				public bool AllowOnlyOne
				{
					get
					{
						if (_AllowOnlyOne.HasValue)
							return _AllowOnlyOne.Value;
						else
							return false;
					}

					set
					{
						_AllowOnlyOne = value;
					}
				}

				public bool AllowOnlyOneSpecified
				{
					get
					{
						return _AllowOnlyOne.HasValue;
					}
				}

				private bool? _AllowOnlyOne;

				public App(App copy)
				{
					Name = copy.Name;
					UniqueID = copy.UniqueID;
					Time = copy.Time;
					StartCounter = copy.StartCounter;
					IsShell = copy.IsShell;
					Rules = copy.Rules;
					_AllowOnlyOne = copy._AllowOnlyOne;
				}
			}

			public struct AppRuleSet
			{
				public RuleSet Kind;
				public string UniqueId;
				public RulePriority Priority;

				public AppRule[] Rules;
			}

			public enum RulePriority
			{
				None,
				Low,
				Medium,
				High,
			}

			public enum RuleSet
			{
				Any,
				All
			}

			public struct AppRule
			{
				public AppRuleMatchTo MatchTo;
				public string MatchString;
				public AppRuleAlgorithm MatchAlgorithm;

				public AppRule(AppRuleMatchTo mt, string str, AppRuleAlgorithm a)
				{
					MatchTo = mt;
					MatchString = str;
					MatchAlgorithm = a;
				}
			}

			public enum AppRuleMatchTo
			{
				FileName,
				FileNamePath,
				FilePath,
				FileVersionName,
				FileVersionDesc,
				FileVersionCompany,
				FileVersionProductVersion,
				FileVersionFileVersion,
				FileMD5,
			}

			public enum AppRuleAlgorithm
			{
				Exact,
				ExactInvariant,
				Near,
				NearInvariant,
				Regex,
				RegexInvariant,
				StartsWith,
				StartsWithInvariant,
				EndWith,
				EndWithInvariant,
			}

			public struct AppTrack
			{
				public ulong EventID;
				public DateTime BeginTime, EndTime;
				public string UniqueID;
				public ulong Time;
			}

			internal struct CurrentApps
			{
				public int PID;
				public string UniqueId;
				public AppRulePair RuleTriggered;
				public ulong StartTime;
				public ulong AllTime;
				public ulong StartCount;
				public App App;
			}

			internal struct AppRulePair
			{
				public string UniqueID;
				public string RuleSetID;
				public RulePriority Priority;

				public AppRulePair(string id, string gid, RulePriority p)
				{
					UniqueID = id;
					RuleSetID = gid;
					Priority = p;
				}
			}

			internal struct ExeData
			{
				public int PID;
				public string Name;
				public string Path;
				public FileVersionInfo FileVersion;
			}

			internal struct ExeDataContainer
			{
				public ExeDataContainerReason Reason;
				private ExeData? data;

				public ExeDataContainer(ExeData ed)
				{
					Reason = ExeDataContainerReason.Valid;
					data = ed;
				}

				public ExeDataContainer(ExeDataContainerReason reason)
				{
					Reason = reason;
					data = null;
				}

				public ExeData Value
				{
					get
					{
						if (data.HasValue)
							return data.Value;
						else
							throw new InvalidOperationException("We don't have value!");
					}

					set
					{
						Reason = ExeDataContainerReason.Valid;
						data = value;
					}
				}
			}

			internal enum ExeDataContainerReason
			{
				Valid,
				IncompleteInformation,
				ExceptionOccured,
			}

			internal struct WaitStruct
			{
				public int PID;
				public ulong StartTime;
				public int Count;
			}
		}

		internal static class Utils
		{
			public static bool IsStringMatch(string pattern, string subject,
				Structs.AppRuleAlgorithm algo)
			{
				switch (algo)
				{
					case Structs.AppRuleAlgorithm.Exact:
						return pattern == subject;

					case Structs.AppRuleAlgorithm.ExactInvariant:
						return pattern.Equals(subject, StringComparison.CurrentCultureIgnoreCase);

					case Structs.AppRuleAlgorithm.Near:
						return new Regex(GenerateNearCharacters(pattern)).IsMatch(subject);

					case Structs.AppRuleAlgorithm.NearInvariant:
						return new Regex(GenerateNearCharacters(pattern), RegexOptions.IgnoreCase).IsMatch(subject);

					case Structs.AppRuleAlgorithm.Regex:
						{
							try
							{
								return new Regex(pattern).IsMatch(subject);
							}
							catch (Exception)
							{
								return false;
							}
						}

					case Structs.AppRuleAlgorithm.RegexInvariant:
						{
							try
							{
								return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(subject);
							}
							catch (Exception)
							{
								return false;
							}
						}

					case Structs.AppRuleAlgorithm.StartsWith:
						return subject.StartsWith(pattern);

					case Structs.AppRuleAlgorithm.StartsWithInvariant:
						return subject.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase);

					case Structs.AppRuleAlgorithm.EndWith:
						return subject.EndsWith(pattern);

					case Structs.AppRuleAlgorithm.EndWithInvariant:
						return subject.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase);
				}

				throw new ArgumentException("Algorithm is not supported", nameof(algo));
			}

			private static string GenerateNearCharacters(string pattern)
			{
				// ? -> .{1}
				// * -> .*

				string out_str = "";
				int it = 0;

				while (it < pattern.Length)
				{
					int asterix = pattern.IndexOf("*", it);
					int exclam = pattern.IndexOf("?", it);

					int char_ = 0;

					if (asterix != -1 && exclam != -1)
						char_ = Math.Min(asterix, exclam);
					else if (asterix != -1)
						char_ = asterix;
					else if (exclam != -1)
						char_ = exclam;
					else
						char_ = -1;

					if (char_ == -1)
					{
						out_str += EscapeCharacters(pattern.Substring(it));
						it = pattern.Length;
					}
					else
					{
						if (char_ != it)
						{
							out_str += EscapeCharacters(pattern.Substring(it, char_ - it));
							it = char_;
						}

						switch (pattern[it])
						{
							case '?':
								out_str += ".{1}";
								break;

							case '*':
								out_str += ".*";
								break;
						}

						it++;
					}
				}

				return "^" + out_str + "$";
			}

			private static string EscapeCharacters(string str)
			{
				foreach (var it in new string[] { "\\", "[", "]", "(", ")", ".", "*", "?", "^", "$" })
					str = str.Replace(it, "\\" + it);

				return str;
			}

			public static string GetTime(ulong time)
			{
				ulong y = 0,
					  o = 0,
					  d = 0,
					  h = 0,
					  m = 0,
					  s = 0;

				const ulong s_time = 1000 * 1,
							m_time = 60 * s_time,
							h_time = 60 * m_time,
							d_time = 24 * h_time,
							o_time = 30 * d_time,
							y_time = 365 * d_time;

				string ret = "";

				if (time > y_time)
				{
					y = time / y_time;
					time -= y * y_time;
				}

				if (time > o_time)
				{
					o = time / o_time;
					time -= o * o_time;
				}

				if (time > d_time)
				{
					d = time / d_time;
					time -= d * d_time;
				}

				if (time > h_time)
				{
					h = time / h_time;
					time -= h * h_time;
				}

				if (time > m_time)
				{
					m = time / m_time;
					time -= m * m_time;
				}

				if (time > s_time)
				{
					s = time / s_time;
					time -= s * s_time;
				}

				if (y > 0)
					ret += y.ToString() + "y ";

				if (o > 0)
					ret += o.ToString() + "mo ";

				if (d > 0)
					ret += d.ToString() + "d ";

				if (h > 0)
					ret += h.ToString() + "h ";

				if (m > 0)
					ret += m.ToString() + "m ";

				if (s > 0)
					ret += s.ToString() + "s ";

				ret = ret.Trim();
				if (ret == string.Empty)
					return "0s";
				else
					return ret;
			}

			internal static string GetMD5(string path)
			{
				string md5_str = "";

				try
				{
					using (MD5 md5 = MD5.Create())
					{
						using (var stream = File.OpenRead(path))
						{
							md5_str = BitConverter.ToString(md5.ComputeHash(stream))
								.Replace("-", "").ToLower();
						}
					}
				}
				catch (Exception)
				{
					md5_str = "can't compute md5 hash!";
				}

				return md5_str;
			}

			internal static string ExtractString(Structs.AppRuleMatchTo matchTo, int pID,
				string name, string path, FileVersionInfo fileVersion,
				ref Dictionary<string, string> md5Cache)
			{
				switch (matchTo)
				{
					case Structs.AppRuleMatchTo.FileName:
						return name;

					case Structs.AppRuleMatchTo.FileNamePath:
						return path;

					case Structs.AppRuleMatchTo.FilePath:
						return Path.GetDirectoryName(path);

					case Structs.AppRuleMatchTo.FileVersionCompany:
						return fileVersion.CompanyName;

					case Structs.AppRuleMatchTo.FileVersionDesc:
						return fileVersion.FileDescription;

					case Structs.AppRuleMatchTo.FileVersionFileVersion:
						return fileVersion.FileVersion;

					case Structs.AppRuleMatchTo.FileVersionName:
						return fileVersion.ProductName;

					case Structs.AppRuleMatchTo.FileVersionProductVersion:
						return fileVersion.ProductVersion;

					case Structs.AppRuleMatchTo.FileMD5:
						if (md5Cache.ContainsKey(path))
							return md5Cache[path];
						else
						{
							string md5 = Utils.GetMD5(path);
							md5Cache[path] = md5;
							return md5;
						}
				}

				throw new ArgumentException("Enumeration argument is wrong!", nameof(matchTo));
			}
		}

		public static class WinAPI
		{
			public const int WM_QUERYENDSESSION = 0x0011;
			public const int WM_ENDSESSION = 0x0016;

			/// <summary>
			/// The WM_KEYDOWN message is posted to the window with the
			/// keyboard focus when a nonsystem key is pressed. A nonsystem
			/// key is a key that is pressed when the ALT key is not pressed.
			/// </summary>
			public const int WM_KEYDOWN = 0x100;

			/// <summary>
			/// The WM_KEYUP message is posted to the window with the keyboard
			/// focus when a nonsystem key is released. A nonsystem key is a
			/// key that is pressed when the ALT key is not pressed, or a
			/// keyboard key that is pressed when a window has the keyboard
			/// focus.
			/// </summary>
			public const int WM_KEYUP = 0x101;

			/// <summary>
			/// The WM_SYSKEYDOWN message is posted to the window with the
			/// keyboard focus when the user presses the F10 key (which
			/// activates the menu bar) or holds down the ALT key and then
			/// presses another key. It also occurs when no window currently
			/// has the keyboard focus; in this case, the WM_SYSKEYDOWN message
			/// is sent to the active window. The window that receives the
			/// message can distinguish between these two contexts by checking
			/// the context code in the lParam parameter.
			/// </summary>
			public const int WM_SYSKEYDOWN = 0x104;

			/// <summary>
			/// The WM_SYSKEYUP message is posted to the window with the
			/// keyboard focus when the user releases a key that was pressed
			/// while the ALT key was held down. It also occurs when no window
			/// currently has the keyboard focus; in this case, the WM_SYSKEYUP
			/// message is sent to the active window. The window that receives
			/// the message can distinguish between these two contexts by
			/// checking the context code in the lParam parameter.
			/// </summary>
			public const int WM_SYSKEYUP = 0x105;

			/// <summary>
			/// Installs a hook procedure that monitors low-level mouse
			/// input events.
			/// </summary>
			public const int WH_MOUSE_LL = 14;

			/// <summary>
			/// Installs a hook procedure that monitors low-level keyboard
			/// input events.
			/// </summary>
			public const int WH_KEYBOARD_LL = 13;

			public const int WM_MOUSEMOVE = 0x0200;
			public const int WM_MOUSEWHEEL = 0x020A;
			public const int WM_MOUSEHWHEEL = 0x020E;

			public const int WM_LBUTTONDOWN = 0x0201;
			public const int WM_LBUTTONUP = 0x0202;

			public const int WM_RBUTTONDOWN = 0x0204;
			public const int WM_RBUTTONUP = 0x0205;

			public const int WM_MBUTTONDOWN = 0x0207;
			public const int WM_MBUTTONUP = 0x0208;

			public const int WM_XBUTTONDOWN = 0x020B;
			public const int WM_XBUTTONUP = 0x020C;

			public const uint EVENT_MIN = 0;
			public const uint EVENT_SYSTEM_FOREGROUND = 0x03;
			public const uint EVENT_OBJECT_NAMECHANGE = 0x800C;
			public const uint EVENT_MAX = 0x7fffffff;

			public const uint WINEVENT_INCONTEXT = 0x4;
			public const uint WINEVENT_OUTOFCONTEXT = 0x1;
			public const uint WINEVENT_SKIPOWNPROCESS = 0x2;
			public const uint WINEVENT_SKIPOWNTHREAD = 0x0;

			public const int OBJID_WINDOW = 0x00000000;
			public const int CHILDID_SELF = 0x00000000;

			public const int WHEEL_DELTA = 120;

			public enum NetConnectionStatus : UInt16
			{
				Disconnected = (0),
				Connecting = (1),
				Connected = (2),
				Disconnecting = (3),
				HardwareNotPresent = (4),
				HardwareDisabled = (5),
				HardwareMalfunction = (6),
				MediaDisconnected = (7),
				Authenticating = (8),
				AuthenticationSucceeded = (9),
				AuthenticationFailed = (10),
				InvalidAddress = (11),
				CredentialsRequired = (12),
				Other,
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct KBDLLHOOKSTRUCT
			{
				public uint vkCode;
				public uint scanCode;
				public KBDLLHOOKSTRUCTFlags flags;
				public uint time;
				public UIntPtr dwExtraInfo;
			}

			[Flags]
			public enum KBDLLHOOKSTRUCTFlags
			{
				LLKHF_EXTENDED = 0x01,
				LLKHF_INJECTED = 0x10,
				LLKHF_ALTDOWN = 0x20,
				LLKHF_UP = 0x80,
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct MSLLHOOKSTRUCT
			{
				public POINT pt;
				public uint mouseData;
				public MSLLHOOKSTRUCTFlags flags;
				public uint time;
				public UIntPtr dwExtraInfo;
			}

			public static int LOWORD(uint md)
			{
				return (short)(md & 0xFFFF);
			}

			public static int HIWORD(uint md)
			{
				return (short)(((int)md & 0xFFFF0000) >> 16);
			}

			[Flags]
			public enum MSLLHOOKSTRUCTFlags
			{
				LLMHF_INJECTED = 0x00000001,
				LLMHF_LOWER_IL_INJECTED = 0x00000002,
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct POINT
			{
				public int X;
				public int Y;

				public POINT(int x, int y)
				{
					X = x;
					Y = y;
				}

				public POINT(System.Drawing.Point pt) : this(pt.X, pt.Y) { }

				public static implicit operator System.Drawing.Point(POINT p)
				{
					return new System.Drawing.Point(p.X, p.Y);
				}

				public static implicit operator POINT(System.Drawing.Point p)
				{
					return new POINT(p.X, p.Y);
				}
			}

			public enum GetAncestorFlags
			{
				/// <summary>
				/// Retrieves the parent window. This does not include the owner, as it does with the GetParent function.
				/// </summary>
				GetParent = 1,
				/// <summary>
				/// Retrieves the root window by walking the chain of parent windows.
				/// </summary>
				GetRoot = 2,
				/// <summary>
				/// Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent.
				/// </summary>
				GetRootOwner = 3
			}

			public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
			public delegate void WinEventProc(IntPtr hWinEventHook,
				uint eventType, IntPtr hwnd, int idObject, int idChild,
				uint dwEventThread, uint dwmsEventTime);

			[DllImport("kernel32.dll")]
			public static extern ulong GetTickCount64();

			/// <summary>
			/// The CallNextHookEx function passes the hook information to the
			/// next hook procedure in the current hook chain. A hook procedure
			/// can call this function either before or after processing the
			/// hook information.
			/// </summary>
			/// <param name="idHook">
			/// Ignored.
			/// </param>
			/// <param name="nCode">
			/// Specifies the hook code passed to the current hook procedure.
			/// The next hook procedure uses this code to determine how to
			/// process the hook information.
			/// </param>
			/// <param name="wParam">
			/// Specifies the wParam value passed to the current hook procedure.
			/// The meaning of this parameter depends on the type of hook
			/// associated with the current hook chain.
			/// </param>
			/// <param name="lParam">
			/// Specifies the lParam value passed to the current hook procedure.
			/// The meaning of this parameter depends on the type of hook
			/// associated with the current hook chain.
			/// </param>
			/// <returns>
			/// This value is returned by the next hook procedure in the chain.
			/// The current hook procedure must also return this value. The
			/// meaning of the return value depends on the hook type. For more
			/// information, see the descriptions of the individual hook
			/// procedures.
			/// </returns>
			/// <remarks>
			/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
			/// </remarks>
			[DllImport("user32.dll", CharSet = CharSet.Auto,
				CallingConvention = CallingConvention.StdCall)]
			public static extern int CallNextHookEx(
				int idHook,
				int nCode,
				IntPtr wParam,
				IntPtr lParam);

			/// <summary>
			/// Installs an application-defined hook procedure into a hook
			/// chain. You would install a hook procedure to monitor the
			/// system for certain types of events. These events are associated
			/// either with a specific thread or with all threads in the same
			/// desktop as the calling thread.
			/// </summary>
			/// <param name="idHook">
			/// The type of hook procedure to be installed
			/// </param>
			/// <param name="lpfn">
			/// A pointer to the hook procedure. If the dwThreadId parameter
			/// is zero or specifies the identifier of a thread created by a
			/// different process, the lpfn parameter must point to a hook
			/// procedure in a DLL. Otherwise, lpfn can point to a hook
			/// procedure in the code associated with the current process.
			/// </param>
			/// <param name="hMod">
			/// A handle to the DLL containing the hook procedure pointed to
			/// by the lpfn parameter. The hMod parameter must be set to NULL
			/// if the dwThreadId parameter specifies a thread created by the
			/// current process and if the hook procedure is within the code
			/// associated with the current process.
			/// </param>
			/// <param name="dwThreadId">
			/// The identifier of the thread with which the hook procedure is
			/// to be associated. For desktop apps, if this parameter is zero,
			/// the hook procedure is associated with all existing threads
			/// running in the same desktop as the calling thread. For Windows
			/// Store apps, see the Remarks section.
			/// </param>
			/// <returns>
			/// If the function succeeds, the return value is the handle to the hook procedure.
			/// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
			/// </returns>
			/// <remarks>
			/// https://msdn.microsoft.com/pl-pl/library/windows/desktop/ms644990(v=vs.85).aspx
			/// </remarks>
			[DllImport("user32.dll", EntryPoint = "SetWindowsHookEx",
				CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
				SetLastError = true)]
			public static extern int SetWindowsHookEx(int idHook, HookProc lpfn,
				IntPtr hMod, uint dwThreadId);

			/// <summary>
			/// The UnhookWindowsHookEx function removes a hook procedure
			/// installed in a hook chain by the SetWindowsHookEx function.
			/// </summary>
			/// <param name="idHook">
			/// Handle to the hook to be removed. This parameter is a hook
			/// handle obtained by a previous call to SetWindowsHookEx.
			/// </param>
			/// <returns>
			/// If the function succeeds, the return value is nonzero.
			/// If the function fails, the return value is zero. To get
			/// extended error information, call GetLastError.
			/// </returns>
			/// <remarks>
			/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
			/// </remarks>
			[DllImport("user32.dll", CharSet = CharSet.Auto,
				CallingConvention = CallingConvention.StdCall, SetLastError = true)]
			public static extern int UnhookWindowsHookEx(int idHook);


			/// <summary>
			/// The ToAscii function translates the specified virtual-key
			/// code and keyboard state to the corresponding character or
			/// characters. The function translates the code using the input
			/// language and physical keyboard layout identified by the
			/// keyboard layout handle.
			/// </summary>
			/// <param name="uVirtKey">
			/// Specifies the virtual-key code to be translated.
			/// </param>
			/// <param name="uScanCode">
			/// Specifies the hardware scan code of the key to be translated.
			/// The high-order bit of this value is set if the key is up
			/// (not pressed).
			/// </param>
			/// <param name="lpbKeyState">
			/// Pointer to a 256-byte array that contains the current
			/// keyboard state. Each element (byte) in the array contains the
			/// state of one key. If the high-order bit of a byte is set, the
			/// key is down (pressed). The low bit, if set, indicates that the
			/// key is toggled on. In this function, only the toggle bit of
			/// the CAPS LOCK key is relevant. The toggle state of the NUM LOCK
			/// and SCROLL LOCK keys is ignored.
			/// </param>
			/// <param name="lpwTransKey">
			/// Pointer to the buffer that receives the translated character
			/// or characters.
			/// </param>
			/// <param name="fuState">
			/// Specifies whether a menu is active. This parameter must be 1
			/// if a menu is active, or 0 otherwise.
			/// </param>
			/// <returns>
			/// If the specified key is a dead key, the return value is
			/// negative. Otherwise, it is one of the following values.
			/// Value Meaning
			/// 0 The specified virtual key has no translation for the current
			/// state of the keyboard.
			/// 1 One character was copied to the buffer.
			/// 2 Two characters were copied to the buffer. This usually
			/// happens when a dead-key character (accent or diacritic) stored
			/// in the keyboard layout cannot be composed with the specified
			/// virtual key to form a single character.
			/// </returns>
			/// <remarks>
			/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
			/// </remarks>
			[DllImport("user32")]
			public static extern int ToAscii(
				uint uVirtKey,
				uint uScanCode,
				byte[] lpbKeyState,
				byte[] lpwTransKey,
				int fuState);

			/// <summary>
			/// The GetKeyboardState function copies the status of the 256
			/// virtual keys to the specified buffer.
			/// </summary>
			/// <param name="pbKeyState">
			/// Pointer to a 256-byte array that contains keyboard key states.
			/// </param>
			/// <returns>
			/// If the function succeeds, the return value is nonzero.
			/// If the function fails, the return value is zero. To get
			/// extended error information, call GetLastError.
			/// </returns>
			/// <remarks>
			/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
			/// </remarks>
			[DllImport("user32")]
			public static extern int GetKeyboardState(byte[] pbKeyState);

			[DllImport("user32")]
			public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax,
				IntPtr hmodWinEventProc, WinEventProc lpfnWinEventProc, int idProcess,
				int idThread, uint dwFlags);

			[DllImport("user32")]
			public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

			[DllImport("kernel32")]
			public static extern int GetProcessId(IntPtr hwndl);

			[DllImport("user32.dll", SetLastError = true)]
			public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

			[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool IsWindow(IntPtr hWnd);

			[DllImport("user32.dll", ExactSpelling = true,
				CharSet = CharSet.Auto, SetLastError = true)]
			public static extern IntPtr GetParent(IntPtr hWnd);

			/// <summary>
			/// Retrieves the handle to the ancestor of the specified window.
			/// </summary>
			/// <param name="hwnd">A handle to the window whose ancestor is to be retrieved.
			/// If this parameter is the desktop window, the function returns NULL. </param>
			/// <param name="flags">The ancestor to be retrieved.</param>
			/// <returns>The return value is the handle to the ancestor window.</returns>
			[DllImport("user32.dll", ExactSpelling = true)]
			public static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags flags);

			[DllImport("user32")]
			public static extern int GetDoubleClickTime();
		}

		internal class Tracker : IDisposable
		{
			internal delegate void ProcesSpawnedType(int pid);
			internal delegate void ProcesEndedType(int pid);
			internal delegate void InternetChangeStateType(Guid guid, string Name, WinAPI.NetConnectionStatus state);
			internal delegate void OSChangeType(ulong free, ulong all, ulong virtualFree, ulong virtualAll);
			internal delegate void ProcessorLoad(int proc);

			internal ProcesSpawnedType OnCreate;
			internal ProcesEndedType OnDelete;
			internal InternetChangeStateType OnInternetEvent;
			internal OSChangeType OnOsEvent;
			internal ProcessorLoad OnProcessorLoad;

			ManagementEventWatcher eventCreated;
			ManagementEventWatcher eventDestroyed;
			ManagementEventWatcher eventInternet;
			ManagementEventWatcher eventOS;
			ManagementEventWatcher eventProcessor;

			internal KeyboardHook kHook = new KeyboardHook();
			internal ForegroundHook fHook = new ForegroundHook();
			internal NamechangeHook nHook = new NamechangeHook();
			internal MouseHook mHook = new MouseHook();

			public void Start()
			{
				GrabAll();

				InitializeTracking();
			}

			private void InitializeTracking()
			{
				const string NameSpace = @"\\.\root\CIMV2";
				const string CreateSql = @"SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";
				const string DeleteSql = @"SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";
				const string NetChange = @"SELECT * FROM __InstanceModificationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_NetworkAdapter' AND TargetInstance.PhysicalAdapter = True";
				const string OperatingSystem = @"SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_OperatingSystem'";
				const string ProcesSql = @"SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_Processor'";

				eventCreated = new ManagementEventWatcher(NameSpace, CreateSql);
				eventCreated.EventArrived += OnCreateProcessEvent;

				eventDestroyed = new ManagementEventWatcher(NameSpace, DeleteSql);
				eventDestroyed.EventArrived += OnDeleteProcessEvent;

				eventInternet = new ManagementEventWatcher(NameSpace, NetChange);
				eventInternet.EventArrived += OnModificationInternetEvent;

				eventOS = new ManagementEventWatcher(NameSpace, OperatingSystem);
				eventOS.EventArrived += OnOSEvent;

				eventProcessor = new ManagementEventWatcher(NameSpace, ProcesSql);
				eventProcessor.EventArrived += ProcessorEvent;

				kHook.Init();
				fHook.Init();
				nHook.Init();
				mHook.Init();

				PullAllInternet();

				eventCreated.Start();
				eventDestroyed.Start();
				eventInternet.Start();
				eventOS.Start();
				eventProcessor.Start();
			}

			private void OnCreateProcessEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				int pid = (int)(UInt32)mbo.Properties["ProcessId"].Value;

				OnCreate(pid);
			}

			private void OnDeleteProcessEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				int pid = (int)(UInt32)mbo.Properties["ProcessId"].Value;

				OnDelete(pid);
			}

			private void OnModificationInternetEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				string name = (string)mbo.Properties["Name"].Value;
				int status = (int)(UInt16)mbo.Properties["NetConnectionStatus"].Value;
				Guid g = Guid.Parse((string)mbo.Properties["GUID"].Value);
				WinAPI.NetConnectionStatus n = status.ToNet();

				OnInternetEvent(g, name, n);
			}

			private void OnOSEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				ulong FreePhysicialMemory = (ulong)(UInt64)mbo.Properties["FreePhysicalMemory"].Value;
				ulong TotalVisibleMemorySize = (ulong)(UInt64)mbo.Properties["TotalVisibleMemorySize"].Value;
				ulong FreeVirtualMemory = (ulong)(UInt64)mbo.Properties["FreeVirtualMemory"].Value;
				ulong TotalVirtualMemorySize = (ulong)(UInt64)mbo.Properties["TotalVirtualMemorySize"].Value;

				OnOsEvent(FreePhysicialMemory, TotalVisibleMemorySize, FreeVirtualMemory, TotalVirtualMemorySize);
			}

			private void ProcessorEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				int LoadPrecentage = (int)(UInt16)mbo.Properties["LoadPercentage"].Value;

				OnProcessorLoad(LoadPrecentage);
			}

			public void GrabAll()
			{
				const string NameSpace = @"root\CIMV2";
				const string Query = @"SELECT * FROM Win32_Process";

				using (ManagementObjectSearcher mos = new ManagementObjectSearcher(NameSpace, Query))
				{
					foreach (ManagementObject mo in mos.Get())
					{
						if (mo["ProcessId"] != null)
						{
							int id = (int)(UInt32)mo["ProcessId"];

							if (id != 0)
								OnCreate(id);
						}
					}
				}
			}

			public void PullAllInternet()
			{
				const string NameSpace = @"root\CIMV2";
				const string Query = @"SELECT Name,NetConnectionStatus,GUID FROM Win32_NetworkAdapter WHERE PhysicalAdapter = True";

				using (ManagementObjectSearcher mos = new ManagementObjectSearcher(NameSpace, Query))
				{
					foreach (ManagementObject mbo in mos.Get())
					{
						string name = (string)mbo.Properties["Name"].Value;
						int status = (int)(UInt16)mbo.Properties["NetConnectionStatus"].Value;
						Guid g = Guid.Parse((string)mbo.Properties["GUID"].Value);
						WinAPI.NetConnectionStatus n = status.ToNet();

						OnInternetEvent(g, name, n);
					}
				}
			}

			public void Finish()
			{
				eventCreated.Stop();
				eventDestroyed.Stop();
				eventInternet.Stop();
				eventOS.Stop();

				kHook.DeInit();
				fHook.DeInit();
				nHook.DeInit();
				mHook.DeInit();

				eventCreated.Dispose();
				eventDestroyed.Dispose();
				eventInternet.Dispose();
				eventOS.Dispose();

				eventInternet = eventCreated = eventDestroyed = null;
			}

			public void Dispose()
			{
				Finish();
			}
		}

		internal static TrackSystem TrackingSystemState = null;

		Tracker tracker = null;
		List<Structs.CurrentApps> currentApps { get; set; } = new List<Structs.CurrentApps>();
		List<Structs.App> definedApps = new List<Structs.App>();
		List<Structs.WaitStruct> waitedApps = new List<Structs.WaitStruct>();
		XmlWriter xmlTracker = null;
		FileStream mouseData = null;
		FileStream keyData = null;
		System.Timers.Timer waited_timer, valid_timer, tick_timer;
		object inOutLock = new object();
		bool valid_tick_running = false;
		bool waited_timer_running = false;
		TimeSpan mouseDoubleClick;

		public TrackSystem()
		{
			mouseDoubleClick = new TimeSpan(0, 0, 0, 0, WinAPI.GetDoubleClickTime());
			LoadState();
			BeginTracking();

			waited_timer = new System.Timers.Timer();
			waited_timer.Interval = 5 * 1000;
			waited_timer.Elapsed += OnWaitTick;
			waited_timer.AutoReset = true;
			waited_timer.Enabled = true;

			valid_timer = new System.Timers.Timer();
			valid_timer.Interval = 1 * 60 * 1000;
			valid_timer.Elapsed += OnValidTick;
			valid_timer.AutoReset = true;
			valid_timer.Enabled = true;

			tick_timer = new System.Timers.Timer();
#if DEBUG
			tick_timer.Interval = 1000;
#else
			tick_timer.Interval = 15 * 60 * 1000;
#endif

			tick_timer.Elapsed += OnTickTick;
			tick_timer.AutoReset = true;
			tick_timer.Enabled = true;
		}

		public void Close()
		{
			waited_timer.Enabled = false;
			valid_timer.Enabled = false;
			tick_timer.Enabled = false;

			ValidTick();
			WaitTick();

			FinishTracking();
			FinishProcess();
			SaveState();
		}

		private void OnValidTick(object sender, EventArgs e)
		{
			if (valid_tick_running)
				return;

			ValidTick();
		}

		private void ValidTick()
		{
			lock (inOutLock)
			{
				valid_tick_running = true;

				foreach (var it in currentApps)
				{
					if (PidNotRunning(it.PID))
					{
						StopApp(it.PID, it);
						currentApps.Remove(it);
						ValidTick();
						return;
					}
				}

				valid_tick_running = false;
			}
		}

		private void OnTickTick(object sender, EventArgs e)
		{
			lock (inOutLock)
			{
				xmlTracker.Node_Ping(DateTime.Now);
			}

			tracker.PullAllInternet();
		}

		private bool PidNotRunning(int PID)
		{
			return GetProcessData(PID).Reason != Structs.ExeDataContainerReason.Valid;
		}

		private void OnWaitTick(object sender, EventArgs e)
		{
			if (waited_timer_running)
				return;

			WaitTick();
		}

		private void WaitTick()
		{
			lock (inOutLock)
			{
				waited_timer_running = true;

				var copy = waitedApps.ToList();
				waitedApps.Clear();

				foreach (var it in copy)
					NewProcessArrived(it.PID, it.Count, it.StartTime);

				waited_timer_running = false;
			}
		}

		private void LoadState()
		{
			try
			{
				XmlReader xr = XmlReader.Create(ApplicationDefinitions);

				XmlSerializer xs = new XmlSerializer(definedApps.GetType());
				definedApps = (List<Structs.App>)xs.Deserialize(xr);
			}
			catch (Exception)
			{
			}

			if (!Directory.Exists(TrackedTimesCatalogue))
				Directory.CreateDirectory(TrackedTimesCatalogue);

			XmlWriterSettings xws = new XmlWriterSettings();
			xws.CloseOutput = true;
			xws.Encoding = Encoding.UTF8;
			xws.Indent = true;
			xws.IndentChars = "\t";

			string baseName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss.fff");
			string xmlFileName =  baseName + ".xml";
			string mdFileName = baseName + ".mousedata";
			string kdFileName = baseName + ".keydata";

			xmlTracker = XmlWriter.Create(Path.Combine(TrackedTimesCatalogue, xmlFileName), xws);
			xmlTracker.WriteStartDocument(true);
			xmlTracker.WriteStartElement("root");
			xmlTracker.WriteAttributeString("start-date", DateTime.Now.ToSensibleFormat());

			string CurrentVersion = "";
			{
				var ass = System.Reflection.Assembly.GetExecutingAssembly();
				var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(ass.Location);
				var str = fvi.ProductVersion.Split('.');
				int it = str.Length - 1;

				while (it >= 0 && str[it] == "0")
					it--;

				for (int kt = 0; kt <= it; ++kt)
					CurrentVersion += str[kt] + '.';

				CurrentVersion = CurrentVersion.Trim('.');
			}
			xmlTracker.WriteAttributeString("version", CurrentVersion);

			mouseData = File.Open(Path.Combine(TrackedTimesCatalogue, mdFileName),
				FileMode.Create, FileAccess.Write, FileShare.Read);

			keyData = File.Open(Path.Combine(TrackedTimesCatalogue, kdFileName),
				FileMode.Create, FileAccess.Write, FileShare.Read);

			{
				byte[] HEADER = "MOUSE DATA LOG 1".GetBytes();
				mouseData.Write(HEADER, 0, HEADER.Length);

				HEADER = "KEYBOARD DATA  1".GetBytes();
				keyData.Write(HEADER, 0, HEADER.Length);
			}
		}

		private void SaveState()
		{
			xmlTracker.WriteEndElement();
			xmlTracker.WriteEndDocument();
			xmlTracker.Close();

			mouseData.Close();
			keyData.Close();

			SaveDatabase();
		}

		public void BeginTracking()
		{
			tracker = new Tracker();

			tracker.OnCreate = NewProcessArrived;
			tracker.OnDelete = ProcessDestoryed;
			tracker.OnInternetEvent = InternetEvent;
			tracker.OnOsEvent = MemoryEvent;
			tracker.OnProcessorLoad = ProcessorLoad;

			tracker.kHook.keyEvent = KeyEvent;
			tracker.fHook.foregroundChanged = ForegroundEvent;
			tracker.nHook.namechangeEvent = NamechangeEvent;

			tracker.mHook.mouseClickEvent = MouseClickEvent;
			tracker.mHook.mouseMoveEvent = MouseMoveEvent;
			tracker.mHook.mouseWheelMoveEvent = MouseWheelEvent;

			var x = System.Windows.Forms.Screen.PrimaryScreen;
			ResolutionChangeEvent(x.Bounds.Width, x.Bounds.Height);

			tracker.Start();
		}

		private void ProcessorLoad(int proc)
		{
			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				xmlTracker.Node_ProcessorLoad(x, proc);
			}
		}

		private void ResolutionChangeEvent(int width, int height)
		{
			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				xmlTracker.Node_ResolutionChanged(x, width, height);
			}
		}

		const byte MouseClick = (byte)'C';
		const byte MouseMove = (byte)'M';
		const byte MouseWheel = (byte)'W';

#if DEBUG
		byte[] MouseBuffer = new byte[32];
#else
		byte[] MouseBuffer = new byte[1 + 8 + 4 + 4 + 4 + 4];
#endif

		DateTime LastMouseAction;

		private void MouseClickEvent(MouseHook.MouseButton btn, bool pressed,
			int x, int y)
		{
			DateTime dt = DateTime.Now;
			byte[] arr;

			Array.Clear(MouseBuffer, 0, MouseBuffer.Length);

			// id
			MouseBuffer[0] = MouseClick;

			// time
			arr = BitConverter.GetBytes(dt.Ticks);
			arr.CopyTo(MouseBuffer, 1);

			// X
			arr = BitConverter.GetBytes(x);
			arr.CopyTo(MouseBuffer, 8 + 1);

			// Y
			arr = BitConverter.GetBytes(y);
			arr.CopyTo(MouseBuffer, 8 + 1 + 4);

			// button
			arr = BitConverter.GetBytes((int)btn);
			arr.CopyTo(MouseBuffer, 8 + 1 + 4 + 4);

			// pressed
			arr = BitConverter.GetBytes(pressed);
			arr.CopyTo(MouseBuffer, 8 + 1 + 4 + 4 + 4);

			mouseData.Write(MouseBuffer, 0, MouseBuffer.Length);

			LastMouseAction = dt;
		}

		private void MouseMoveEvent(int x, int y)
		{
			DateTime dt = DateTime.Now; byte[] arr;

			if (LastMouseAction.AddMilliseconds(100) >= dt)
				return;

			Array.Clear(MouseBuffer, 0, MouseBuffer.Length);

			// id
			MouseBuffer[0] = MouseMove;

			// time
			arr = BitConverter.GetBytes(dt.Ticks);
			arr.CopyTo(MouseBuffer, 1);

			// X
			arr = BitConverter.GetBytes(x);
			arr.CopyTo(MouseBuffer, 8 + 1);

			// Y
			arr = BitConverter.GetBytes(y);
			arr.CopyTo(MouseBuffer, 8 + 1 + 4);

			mouseData.Write(MouseBuffer, 0, MouseBuffer.Length);

			LastMouseAction = dt;
		}

		private void MouseWheelEvent(MouseHook.MouseAxis axis, int value,
			int x, int y)
		{
			DateTime dt = DateTime.Now; byte[] arr;

			Array.Clear(MouseBuffer, 0, MouseBuffer.Length);

			// id
			MouseBuffer[0] = MouseWheel;

			// time
			arr = BitConverter.GetBytes(dt.Ticks);
			arr.CopyTo(MouseBuffer, 1);

			// X
			arr = BitConverter.GetBytes(x);
			arr.CopyTo(MouseBuffer, 8 + 1);

			// Y
			arr = BitConverter.GetBytes(y);
			arr.CopyTo(MouseBuffer, 8 + 1 + 4);

			// button
			arr = BitConverter.GetBytes((int)axis);
			arr.CopyTo(MouseBuffer, 8 + 1 + 4 + 4);

			// pressed
			arr = BitConverter.GetBytes(value);
			arr.CopyTo(MouseBuffer, 8 + 1 + 4 + 4 + 4);

			mouseData.Write(MouseBuffer, 0, MouseBuffer.Length);

			LastMouseAction = dt;
		}

		uint LastNameChangePID = 0;
		string LastNameChangeTitle = "";

		private void NamechangeEvent(uint PID, string winTitle)
		{
			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				if (PID == LastNameChangePID && winTitle == LastNameChangeTitle)
					return;

				LastNameChangePID = PID;
				LastNameChangeTitle = winTitle;

				xmlTracker.Node_NameChange(x, PID, winTitle);
			}
		}

		uint LastForegroundProcessID = 0;

		private void ForegroundEvent(uint ThreadId, uint ProcessID)
		{
			if (LastForegroundProcessID == ProcessID)
				return;

			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				xmlTracker.Node_WindowForegroundChanged(x, ProcessID);
			}

			LastForegroundProcessID = ProcessID;
		}

		const byte KeyboardPress = (byte)'P';
		const byte KeyboardUnpress = (byte)'U';

		byte[] KeyboardBuffer = new byte[1 + 8 + 8];

		private void KeyEvent(uint virtualKode, uint scanKode, bool up)
		{
			DateTime dt = DateTime.Now; byte[] arr;

			Array.Clear(KeyboardBuffer, 0, KeyboardBuffer.Length);

			// id
			if (up)
				KeyboardBuffer[0] = KeyboardPress;
			else
				KeyboardBuffer[0] = KeyboardUnpress;

			// time
			arr = BitConverter.GetBytes(dt.Ticks);
			arr.CopyTo(KeyboardBuffer, 1);

			// X
			arr = BitConverter.GetBytes(virtualKode);
			arr.CopyTo(KeyboardBuffer, 8 + 1);

			// Y
			arr = BitConverter.GetBytes(scanKode);
			arr.CopyTo(KeyboardBuffer, 8 + 1 + 4);

			keyData.Write(KeyboardBuffer, 0, KeyboardBuffer.Length);
		}

		int LastAllMemoryRecorded = 0;

		private void MemoryEvent(ulong free, ulong all, ulong vfree, ulong vall)
		{
			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				// physicial
				double ptotal = all;
				double pfree = free;
				string pround = (Math.Round(((ptotal - pfree) / ptotal * 100), 2)).ToString();

				// virtual
				double vtotal = vall;
				double vfrre = vfree;
				string vround = (Math.Round(((vtotal - vfrre) / vtotal * 100), 2)).ToString();

				// all
				double atotal = ptotal + vtotal;
				double afree = pfree + vfree;
				string round = (Math.Round(((atotal - afree) / atotal * 100), 2)).ToString();

				int allMemCurrent = (int)Math.Round(((atotal - afree) / atotal * 100), 2);

				if (LastAllMemoryRecorded == allMemCurrent)
					return;

				LastAllMemoryRecorded = allMemCurrent;

				ulong pvtotal = all + vall;
				ulong pvfree = free + vfree;

				xmlTracker.Node_MemoryInfo(x, free, all, pround,
					vfree, vall, vround,
					pvfree, pvtotal, round);
			}
		}

		private void InternetEvent(Guid g, string Name, WinAPI.NetConnectionStatus state)
		{
			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				xmlTracker.Node_NetworkAdapterEvent(x, Name, g, state);
			}
		}

		private void NewProcessArrived(int pid)
		{
			NewProcessArrived(pid, 0, WinAPI.GetTickCount64());
		}

		private void NewProcessArrived(int pid, int retire, ulong processTime)
		{
			if (pid == 0 || pid == 4)
				return;

			lock (inOutLock)
			{
				// podany PID już jest już śledzony
				if (currentApps.Contains(cai => cai.PID == pid))
					return;

				// pobieramy dane o procesie
				var pd = GetProcessData(pid);

				// nie udało się pobrać danych?
				switch (pd.Reason)
				{
					case Structs.ExeDataContainerReason.IncompleteInformation:
						AddToWaitQueue(pid, processTime, retire);
						return;

					case Structs.ExeDataContainerReason.ExceptionOccured:
						return;
				}

				var ipd = pd.Value;

				ScanForApplication(ipd.PID, ipd.Name, ipd.Path, ipd.FileVersion,
					processTime);
			}
		}

		private Structs.ExeDataContainer GetProcessData(int pid)
		{
			try
			{
				Structs.ExeData ed;

				var pinfo = Process.GetProcessById(pid);

				ed.PID = pid;
				ed.Path = pinfo.MainModule.FileName;
				ed.Name = Path.GetFileName(ed.Path);
				ed.FileVersion = FileVersionInfo.GetVersionInfo(ed.Path);

				return new Structs.ExeDataContainer(ed);
			}
			catch (Win32Exception w32) when (w32.NativeErrorCode == 299)
			{
				return new Structs.ExeDataContainer(Structs.ExeDataContainerReason.IncompleteInformation);
			}
			catch (Exception)
			{
				return new Structs.ExeDataContainer(Structs.ExeDataContainerReason.ExceptionOccured);
			}
		}

		private void AddToWaitQueue(int pid, ulong processTime, int count)
		{
			if (count > 5)
				return;

			lock (inOutLock)
			{
				Structs.WaitStruct ws = new Structs.WaitStruct();

				ws.Count = count;
				ws.PID = pid;
				ws.StartTime = processTime;

				waitedApps.Add(ws);
			}
		}

		private void ScanForApplication(int PID, string name, string path,
			FileVersionInfo fileVersion, ulong processTime)
		{
			lock (inOutLock)
			{
				List<Structs.AppRulePair> qualified = new List<Structs.AppRulePair>();
				Dictionary<string, string> md5Cache = new Dictionary<string, string>();
				Structs.RulePriority definedPriority = Structs.RulePriority.None;

				foreach (var it in definedApps)
				{
					if (it.Rules == null)
						continue;

					foreach (var jt in it.Rules)
					{
						int rules_match = 0;

						if (definedPriority > jt.Priority)
							continue;

						foreach (var kt in jt.Rules)
						{
							if (Utils.IsStringMatch(kt.MatchString,
								Utils.ExtractString(kt.MatchTo, PID, name, path,
									fileVersion, ref md5Cache),
								kt.MatchAlgorithm))
								rules_match++;
							else
							{
								if (jt.Kind == Structs.RuleSet.All)
									break;
							}
						}

						if (jt.Kind == Structs.RuleSet.All &&
							rules_match == jt.Rules.Length)
						{
							qualified.Add(new Structs.AppRulePair(it.UniqueID,
								jt.UniqueId, jt.Priority));

							if (definedPriority > jt.Priority)
								definedPriority = jt.Priority;
						}
						else if (jt.Kind == Structs.RuleSet.Any && rules_match > 0)
						{
							qualified.Add(new Structs.AppRulePair(it.UniqueID,
								jt.UniqueId, jt.Priority));

							if (definedPriority > jt.Priority)
								definedPriority = jt.Priority;
						}
					}
				}

				if (qualified.Count == 0)
					return;

				qualified = qualified.OrderByDescending(x => x.Priority).ToList();
				Structs.App chosen_app = GetAppByRule(qualified[0]);

				StartApp(PID, chosen_app, processTime, chosen_app.Time, qualified[0]);
			}
		}

		public Structs.App AddNewDefinition(string applicationName,
			string applicationUniqueID, Structs.AppRuleSet[] applicationRules,
			bool allowOnlyOne)
		{
			string application_unique_id = applicationUniqueID;
			int counter = 2;

			while (definedApps.Contains(c => c.UniqueID == application_unique_id))
				application_unique_id = string.Format("{0}_{1}", applicationUniqueID, counter++);

			applicationUniqueID = application_unique_id;

			Structs.App app = new Structs.App();

			app.Name = applicationName;
			app.UniqueID = applicationUniqueID;
			app.Rules = applicationRules;
			app.IsShell = false;

			app.StartCounter = 0;
			app.Time = 0;

			app.AllowOnlyOne = allowOnlyOne;

			definedApps.Add(app);

			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				xmlTracker.Node_NewDefinition(x, applicationUniqueID, app.Name);
			}

			return app;
		}

		public void RemoveApp(string text)
		{
			foreach (var it in definedApps)
			{
				if (it.UniqueID == text)
				{
					Structs.App c = it;
					c.IsShell = true;
					c.Rules = null;

					lock (inOutLock)
					{
						DateTime x = DateTime.Now;

						xmlTracker.Node_RemoveDefinition(x, c.UniqueID);
					}

					definedApps.Remove(it);
					definedApps.Add(c);
					return;
				}
			}
		}

		public void UpdateApp(string uniqueID, Structs.App idata)
		{
			definedApps.Replace(p => p.UniqueID == uniqueID, idata);
		}

		public Structs.App GetAppById(string AppID)
		{
			return (from t in definedApps where t.UniqueID == AppID select t).First();
		}

		public List<Structs.App> GetDefiniedApps()
		{
			return (from t in definedApps where t.IsShell == false select t).ToList();
		}

		private Structs.App GetAppByRule(Structs.AppRulePair appRulePair)
		{
			return GetAppById(appRulePair.UniqueID);
		}

		private void StartApp(int PID, Structs.App chosen_app,
			ulong processTime, ulong fullTime, Structs.AppRulePair ruleselected)
		{
			DateTime x = DateTime.Now;

			var n = new Structs.App(chosen_app);
			n.StartCounter++;

			definedApps.Replace(p => p.UniqueID == chosen_app.UniqueID, n);

			Structs.CurrentApps ca = new Structs.CurrentApps();

			ca.PID = PID;
			ca.UniqueId = chosen_app.UniqueID;
			ca.StartTime = processTime;
			ca.AllTime = fullTime;
			ca.RuleTriggered = ruleselected;
			ca.StartCount = n.StartCounter;

			currentApps.Add(ca);

			lock (inOutLock)
			{
				xmlTracker.Node_Begin(x, PID, ruleselected.UniqueID,
					ruleselected.RuleSetID);
			}

			// rejestrujemy nazwe głównego okna
			try
			{
				Process p = Process.GetProcessById(PID);

				lock (inOutLock)
				{
					xmlTracker.Node_NameChange(x, (uint)PID, p.MainWindowTitle);
				}
			} catch (Exception)
			{
			}
		}

		private void ProcessDestoryed(int pid)
		{
			lock (inOutLock)
			{
				// sprawdzamy czy podany proces istnieje w głównym oknie
				var idx = currentApps.FindIndex(p => p.PID == pid);

				if (idx >= 0)
				{
					// główne okno
					StopApp(pid, currentApps[idx]);
					currentApps.RemoveAt(idx);
				}
			}
		}

		private void StopApp(int pid, Structs.CurrentApps current)
		{
			ulong ct = WinAPI.GetTickCount64() - current.StartTime;
			DateTime x = DateTime.Now;

			var idx = definedApps.FindIndex(p => p.UniqueID == current.RuleTriggered.UniqueID);

			Debug.Assert(idx >= 0);

			Structs.App c = definedApps[idx];
			c.Time += ct;
			definedApps[idx] = c;

			// sprawdzamy czy nie dodajemy promocji

			lock (inOutLock)
			{
				xmlTracker.Node_End(x, pid, current.RuleTriggered.UniqueID, ct);
			}
		}

		public void FinishTracking()
		{
			tracker.Dispose();
			tracker = null;
		}

		private void FinishProcess()
		{
			foreach (var it in currentApps)
				StopApp(it.PID, it);
		}

		internal List<Structs.CurrentApps> GetRunningAps()
		{
			List<Structs.CurrentApps> ret = new List<Structs.CurrentApps>(currentApps.Count);

			for (var it = 0; it < currentApps.Count; ++it)
			{
				Structs.CurrentApps copy = currentApps[it];
				copy.App = (from da in definedApps
							where da.UniqueID == copy.UniqueId
							select da).First();
				ret.Add(copy);
			}

			return ret;
		}

		internal void GrabAll()
		{
			tracker.GrabAll();
		}

		internal void SaveDatabase()
		{
			lock (inOutLock)
			{
				try
				{
					definedApps = definedApps.OrderBy(o => o.UniqueID).ToList();

					XmlWriterSettings xws = new XmlWriterSettings();
					xws.CloseOutput = true;
					xws.Encoding = Encoding.UTF8;
					xws.Indent = true;
					xws.IndentChars = "\t";

					using (XmlWriter xw = XmlWriter.Create(ApplicationDefinitions, xws))
					{
						XmlSerializer xs = new XmlSerializer(definedApps.GetType());
						xs.Serialize(xw, definedApps);
					}
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
