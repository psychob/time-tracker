﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WinAPI
{
    public static class User32
    {
        public enum SetWindowsHookType : int
        {
            /// <summary>
            /// Installs a hook procedure that records input messages posted
            /// to the system message queue. This hook is useful for recording
            /// macros
            /// </summary>
            WH_JOURNALRECORD = 0,

            /// <summary>
            /// Installs a hook procedure that posts messages previously
            /// recorded by a WH_JOURNALRECORD hook procedure
            /// </summary>
            WH_JOURNALPLAYBACK = 1,

            /// <summary>
            /// Installs a hook procedure that monitors keystroke messages
            /// </summary>
            WH_KEYBOARD = 2,

            /// <summary>
            /// Installs a hook procedure that monitors messages posted to a
            /// message queue.
            /// </summary>
            WH_GETMESSAGE = 3,

            /// <summary>
            /// Installs a hook procedure that monitors messages before the
            /// system sends them to the destination window procedure.
            /// </summary>
            WH_CALLWNDPROC = 4,

            /// <summary>
            /// Installs a hook procedure that receives notifications useful
            /// to a CBT application
            /// </summary>
            WH_CBT = 5,

            /// <summary>
            /// Installs a hook procedure that monitors messages generated as
            /// a result of an input event in a dialog box, message box, menu,
            /// or scroll bar. The hook procedure monitors these messages for
            /// all applications in the same desktop as the calling thread
            /// </summary>
            WH_SYSMSGFILTER = 6,

            /// <summary>
            /// Installs a hook procedure that monitors mouse messages
            /// </summary>
            WH_MOUSE = 7,

            /// <summary>
            /// Installs a hook procedure useful for debugging other hook
            /// procedures
            /// </summary>
            WH_DEBUG = 9,

            /// <summary>
            /// Installs a hook procedure that receives notifications useful
            /// to shell applications
            /// </summary>
            WH_SHELL = 10,

            /// <summary>
            /// Installs a hook procedure that will be called when the
            /// application's foreground thread is about to become idle. This
            /// hook is useful for performing low priority tasks during idle
            /// time.
            /// </summary>
            WH_FOREGROUNDIDLE = 11,

            /// <summary>
            /// Installs a hook procedure that monitors messages after they
            /// have been processed by the destination window procedure.
            /// </summary>
            WH_CALLWNDPROCRET = 12,

            /// <summary>
            /// Installs a hook procedure that monitors low-level keyboard
            /// input events.
            /// </summary>
            WH_KEYBOARD_LL = 13,

            /// <summary>
            /// Installs a hook procedure that monitors low-level mouse input
            /// events.
            /// </summary>
            WH_MOUSE_LL = 14,

            /// <summary>
            /// Installs a hook procedure that monitors messages generated as
            /// a result of an input event in a dialog box, message box, menu,
            /// or scroll bar.
            /// </summary>
            WH_MSGFILTER = -1,
        }

        /// <summary>
        /// Flag values that specify the location of the hook function and of
        /// the events to be skipped
        /// </summary>
        [Flags]
        public enum SetWinEventHookFlags : uint
        {
            /// <summary>
            /// The DLL that contains the callback function is mapped into the
            /// address space of the process that generates the event. With
            /// this flag, the system sends event notifications to the callback
            /// function as they occur. The hook function must be in a DLL
            /// when this flag is specified. This flag has no effect when both
            /// the calling process and the generating process are not 32-bit
            /// or 64-bit processes, or when the generating process is a
            /// console application.
            /// </summary>
            WINEVENT_INCONTEXT = 0x04,

            /// <summary>
            /// The callback function is not mapped into the address space of
            /// the process that generates the event. Because the hook
            /// function is called across process boundaries, the system must
            /// queue events. Although this method is asynchronous, events
            /// are guaranteed to be in sequential order.
            /// </summary>
            WINEVENT_OUTOFCONTEXT = 0x01,

            /// <summary>
            /// Prevents this instance of the hook from receiving the events
            /// that are generated by threads in this process. This flag does
            /// not prevent threads from generating events.
            /// </summary>
            WINEVENT_SKIPOWNPROCESS = 0x02,

            /// <summary>
            /// Prevents this instance of the hook from receiving the events
            /// that are generated by the thread that is registering this hook.
            /// </summary>
            WINEVENT_SKIPOWNTHREAD = 0x00,
        }

        /// <summary>
        /// This enumeration describes WinEvents
        /// </summary>
        public enum SetWinEventHookType : uint
        {
            /// <summary>
            /// The lowest possible event value
            /// </summary>
            EVENT_MIN = 0x00000001,

            /// <summary>
            /// The highest possible event value
            /// </summary>
            EVENT_MAX = 0x7FFFFFFF,

            /// <summary>
            /// An object's KeyboardShortcut property has changed. Server
            /// applications send this event for their accessible objects.
            /// </summary>
            EVENT_OBJECT_ACCELERATORCHANGE = 0x8012,

            /// <summary>
            /// A window object's scrolling has ended. Unlike
            /// EVENT_SYSTEM_SCROLLEND, this event is associated with the
            /// scrolling window. Whether the scrolling is horizontal or
            /// vertical scrolling, this event should be sent whenever the
            /// scroll action is completed.
            ///
            ///	The hwnd parameter of the WinEventProc callback function
            ///	describes the scrolling window; the idObject parameter is
            ///	OBJID_CLIENT, and the idChild parameter is CHILDID_SELF.
            /// </summary>
            EVENT_OBJECT_CONTENTSCROLLED = 0x8015,

            /// <summary>
            /// The foreground window has changed. The system sends this event
            /// even if the foreground window has changed to another window in
            /// the same thread. Server applications never send this event.
            ///
            /// For this event, the WinEventProc callback function's hwnd
            /// parameter is the handle to the window that is in the
            /// foreground, the idObject parameter is OBJID_WINDOW, and the
            /// idChild parameter is CHILDID_SELF.
            /// </summary>
            EVENT_SYSTEM_FOREGROUND = 0x0003,

            /// <summary>
            /// An object's Name property has changed. The system sends this
            /// event for the following user interface elements: check box,
            /// cursor, list-view control, push button, radio button, status
            /// bar control, tree view control, and window object. Server
            /// applications send this event for their accessible objects.
            /// </summary>
            EVENT_OBJECT_NAMECHANGE = 0x800C,
        }

        public const int OBJID_WINDOW = 0;
        public const int CHILDID_SELF = 0;

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

        /// <summary>
        /// An application-defined or library-defined callback function used
        /// with the SetWindowsHookEx function. The system calls this function
        /// every time a new event is about to be posted into a thread input
        /// queue.
        /// </summary>
        /// <param name="nCode">
        /// A code the hook procedure uses to determine how to process the
        /// message. If nCode is less than zero, the hook procedure must pass
        /// the message to the CallNextHookEx function without further
        /// processing and should return the value returned by CallNextHookEx.
        /// This parameter can be one of the following values.
        /// </param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns>
        /// If nCode is less than zero, the hook procedure must return the
        /// value returned by CallNextHookEx.
        /// If nCode is greater than or equal to zero, and the hook procedure
        /// did not process the message, it is highly recommended that you
        /// call CallNextHookEx and return the value it returns; otherwise,
        /// other applications that have installed WH_MOUSE_LL hooks will not
        /// receive hook notifications and may behave incorrectly as a result.
        /// If the hook procedure processed the message, it may return a
        /// nonzero value to prevent the system from passing the message to
        /// the rest of the hook chain or the target window procedure.
        /// </returns>
        public delegate int SetWindowsHookProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// An application-defined callback (or hook) function that the system
        /// calls in response to events generated by an accessible object. The
        /// hook function processes the event notifications as required.
        /// Clients install the hook function and request specific types of
        /// event notifications by calling SetWinEventHook.
        /// </summary>
        /// <param name="hWinEventHook">
        /// Handle to an event hook function. This value is returned by
        /// SetWinEventHook when the hook function is installed and is specific
        /// to each instance of the hook function.
        /// </param>
        /// <param name="eventType">
        /// Specifies the event that occurred. This value is one of the event
        /// constants.
        /// </param>
        /// <param name="hwnd">
        /// Handle to the window that generates the event, or NULL if no
        /// window is associated with the event. For example, the mouse
        /// pointer is not associated with a window.
        /// </param>
        /// <param name="idObject">
        /// Identifies the object associated with the event. This is one of
        /// the object identifiers or a custom object ID.
        /// </param>
        /// <param name="idChild">
        /// Identifies whether the event was triggered by an object or a child
        /// element of the object. If this value is CHILDID_SELF, the event
        /// was triggered by the object; otherwise, this value is the child ID
        /// of the element that triggered the event.
        /// </param>
        /// <param name="dwEventThread">
        /// Identifies the thread that generated the event, or the thread that
        /// owns the current window.
        /// </param>
        /// <param name="dwmsEventTime">
        /// Specifies the time, in milliseconds, that the event was generated.
        /// </param>
        /// <remarks>
        /// Within the hook function, the parameters hwnd, idObject, and
        /// idChild are used when calling AccessibleObjectFromEvent.
        /// </remarks>
        public delegate void SetWinEventHookProc(IntPtr hWinEventHook,
            uint eventType, IntPtr hwnd, int idObject, int idChild,
            uint dwEventThread, uint dwmsEventTime);

        /// <summary>
        /// Contains information about a low-level keyboard input event.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct KBDLLHOOKSTRUCT
        {
            /// <summary>
            /// A virtual-key code. The code must be a value in the range 1 to
            /// 254.
            /// </summary>
            public uint vkCode;

            /// <summary>
            /// A hardware scan code for the key.
            /// </summary>
            public uint scanCode;

            /// <summary>
            /// The extended-key flag, event-injected flags, context code, and
            /// transition-state flag. This member is specified as follows. An
            /// application can use the following values to test the keystroke
            /// flags. Testing LLKHF_INJECTED (bit 4) will tell you whether the
            /// event was injected. If it was, then testing
            /// LLKHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not
            /// the event was injected from a process running at lower
            /// integrity level.
            /// </summary>
            public KBDLLHOOKSTRUCTFlags flags;

            /// <summary>
            /// The time stamp for this message, equivalent to what
            /// GetMessageTime would return for this message.
            /// </summary>
            public uint time;

            /// <summary>
            /// Additional information associated with the message.
            /// </summary>
            public UIntPtr dwExtraInfo;
        }

        /// <summary>
        /// The extended-key flag, event-injected flags, context code, and
        /// transition-state flag. This member is specified as follows. An
        /// application can use the following values to test the keystroke
        /// flags. Testing LLKHF_INJECTED (bit 4) will tell you whether the
        /// event was injected. If it was, then testing LLKHF_LOWER_IL_INJECTED
        /// (bit 1) will tell you whether or not the event was injected from
        /// a process running at lower integrity level.
        /// </summary>
        [Flags]
        public enum KBDLLHOOKSTRUCTFlags : uint
        {
            /// <summary>
            /// Test the extended-key flag.
            /// </summary>
            LLKHF_EXTENDED = 0x01,

            /// <summary>
            /// Test the event-injected (from a process running at lower
            /// integrity level) flag.
            /// </summary>
            LLKHF_LOWER_IL_INJECTED = 0x00000002,

            /// <summary>
            /// Test the event-injected (from any process) flag.
            /// </summary>
            LLKHF_INJECTED = 0x00000010,

            /// <summary>
            /// Test the context code.
            /// </summary>
            LLKHF_ALTDOWN = 0x20,

            /// <summary>
            /// Test the transition-state flag.
            /// </summary>
            LLKHF_UP = 0x80,
        }

        /// <summary>
        /// Contains information about a low-level mouse input event.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            /// <summary>
            /// The x- and y-coordinates of the cursor, in screen coordinates.
            /// </summary>
            public POINT pt;

            /// <summary>
            /// If the message is WM_MOUSEWHEEL, the high-order word of this
            /// member is the wheel delta. The low-order word is reserved. A
            /// positive value indicates that the wheel was rotated forward,
            /// away from the user; a negative value indicates that the wheel
            /// was rotated backward, toward the user. One wheel click is
            /// defined as WHEEL_DELTA, which is 120.
            ///
            /// If the message is WM_XBUTTONDOWN, WM_XBUTTONUP,
            /// WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, or
            /// WM_NCXBUTTONDBLCLK, the high-order word specifies which X
            /// button was pressed or released, and the low-order word is
            /// reserved.This value can be one or more of the following
            /// values. Otherwise, mouseData is not used.
            /// </summary>
            public uint mouseData;

            /// <summary>
            /// The event-injected flags. An application can use the
            /// following values to test the flags. Testing LLMHF_INJECTED
            /// (bit 0) will tell you whether the event was injected. If it
            /// was, then testing LLMHF_LOWER_IL_INJECTED (bit 1) will tell
            /// you whether or not the event was injected from a process
            /// running at lower integrity level.
            /// </summary>
            public MSLLHOOKSTRUCTFlags flags;

            /// <summary>
            /// The time stamp for this message.
            /// </summary>
            public uint time;

            /// <summary>
            /// Additional information associated with the message.
            /// </summary>
            public UIntPtr dwExtraInfo;
        }

        /// <summary>
        /// The event-injected flags. An application can use the
        /// following values to test the flags. Testing LLMHF_INJECTED
        /// (bit 0) will tell you whether the event was injected. If it
        /// was, then testing LLMHF_LOWER_IL_INJECTED (bit 1) will tell
        /// you whether or not the event was injected from a process
        /// running at lower integrity level.
        /// </summary>
        [Flags]
        public enum MSLLHOOKSTRUCTFlags
        {
            /// <summary>
            /// Test the event-injected (from any process) flag.
            /// </summary>
            LLMHF_INJECTED = 0x00000001,

            /// <summary>
            /// Test the event-injected (from a process running at lower
            /// integrity level) flag
            /// </summary>
            LLMHF_LOWER_IL_INJECTED = 0x00000002,
        }

        /// <summary>
        /// The POINT structure defines the x- and y- coordinates of a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// The x-coordinate of the point.
            /// </summary>
            public int X;

            /// <summary>
            /// The y-coordinate of the point.
            /// </summary>
            public int Y;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="x">
            /// X-coordinate of the point
            /// </param>
            /// <param name="y">
            /// Y-coordinate of the point
            /// </param>
            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="pt">
            /// Point from which get the x,y coordinates
            /// </param>
            public POINT(Point pt) : this(pt.X, pt.Y) { }

            /// <summary>
            /// Implict operator, to convert Sytem.Drawing.Point
            /// </summary>
            /// <param name="p">
            /// POINT
            /// </param>
            public static implicit operator Point(POINT p)
            {
                return new Point(p.X, p.Y);
            }

            /// <summary>
            /// Implict operator, to convert to POINT
            /// </summary>
            /// <param name="p">
            /// Point
            /// </param>
            public static implicit operator POINT(Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain.
        /// You would install a hook procedure to monitor the system for
        /// certain types of events. These events are associated either with a
        /// specific thread or with all threads in the same desktop as the
        /// calling thread.
        /// </summary>
        /// <param name="hookType">
        /// The type of hook procedure to be installed
        /// </param>
        /// <param name="lpfn">
        /// A pointer to the hook procedure. If the dwThreadId parameter is
        /// zero or specifies the identifier of a thread created by a
        /// different process, the lpfn parameter must point to a hook
        /// procedure in a DLL. Otherwise, lpfn can point to a hook procedure
        /// in the code associated with the current process.
        /// </param>
        /// <param name="hMod">
        /// A handle to the DLL containing the hook procedure pointed to by
        /// the lpfn parameter. The hMod parameter must be set to NULL if
        /// the dwThreadId parameter specifies a thread created by the current
        /// process and if the hook procedure is within the code associated
        /// with the current process.
        /// </param>
        /// <param name="dwThreadId">
        /// The identifier of the thread with which the hook procedure is to
        /// be associated. For desktop apps, if this parameter is zero, the
        /// hook procedure is associated with all existing threads running in
        /// the same desktop as the calling thread. For Windows Store apps,
        /// see the Remarks section
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the handle to the
        /// hook procedure.
        ///
        /// If the function fails, the return value is NULL.To get extended
        /// error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms644990(v=vs.85).aspx
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SetWindowsHookEx(
            SetWindowsHookType hookType, SetWindowsHookProc lpfn,
            IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// Passes the hook information to the next hook procedure in the
        /// current hook chain. A hook procedure can call this function either
        /// before or after processing the hook information.
        /// </summary>
        /// <param name="hHook">
        /// This parameter is ignored
        /// </param>
        /// <param name="nCode">
        /// The hook code passed to the current hook procedure. The next hook
        /// procedure uses this code to determine how to process the hook
        /// information.
        /// </param>
        /// <param name="wParam">
        /// The wParam value passed to the current hook procedure. The meaning
        /// of this parameter depends on the type of hook associated with the
        /// current hook chain.
        /// </param>
        /// <param name="lParam">
        /// The lParam value passed to the current hook procedure. The
        /// meaning of this parameter depends on the type of hook associated
        /// with the current hook chain.
        /// </param>
        /// <returns>
        /// This value is returned by the next hook procedure in the chain.
        /// The current hook procedure must also return this value. The
        /// meaning of the return value depends on the hook type. For more
        /// information, see the descriptions of the individual hook
        /// procedures.
        /// </returns>
        /// <remarks>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms644974(v=vs.85).aspx
        /// </remarks>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true)]
        public static extern int CallNextHookEx(IntPtr hHook, int nCode,
            IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Removes a hook procedure installed in a hook chain by the
        /// SetWindowsHookEx function.
        /// </summary>
        /// <param name="hHook">
        /// A handle to the hook to be removed. This parameter is a hook
        /// handle obtained by a previous call to SetWindowsHookEx.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        ///
        /// If the function fails, the return value is zero. To get extended
        /// error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall,
            SetLastError = true, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hHook);

        /// <summary>
        /// Sets an event hook function for a range of events.
        /// </summary>
        /// <param name="eventMin">
        /// Specifies the event constant for the lowest event value in the
        /// range of events that are handled by the hook function. This
        /// parameter can be set to EVENT_MIN to indicate the lowest possible
        /// event value.
        /// </param>
        /// <param name="eventMax">
        /// Specifies the event constant for the highest event value in the
        /// range of events that are handled by the hook function. This
        /// parameter can be set to EVENT_MAX to indicate the highest possible
        /// event value.
        /// </param>
        /// <param name="hmodWinEventProc">
        /// Handle to the DLL that contains the hook function at
        /// lpfnWinEventProc, if the WINEVENT_INCONTEXT flag is specified in
        /// the dwFlags parameter. If the hook function is not located in a DLL,
        /// or if the WINEVENT_OUTOFCONTEXT flag is specified, this
        /// parameter is NULL.
        /// </param>
        /// <param name="lpfnWinEventProc">
        /// Pointer to the event hook function.
        /// </param>
        /// <param name="idProcess">
        /// Specifies the ID of the process from which the hook function
        /// receives events. Specify zero (0) to receive events from all
        /// processes on the current desktop.
        /// </param>
        /// <param name="idThread">
        /// Specifies the ID of the thread from which the hook function
        /// receives events. If this parameter is zero, the hook function is
        /// associated with all existing threads on the current desktop.
        /// </param>
        /// <param name="dwFlags">
        /// Flag values that specify the location of the hook function and of
        /// the events to be skipped.
        /// </param>
        /// <returns>
        /// If successful, returns an HWINEVENTHOOK value that identifies this
        /// event hook instance. Applications save this return value to use
        /// it with the UnhookWinEvent function.
        /// If unsuccessful, returns zero.
        /// </returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true)]
        public static extern IntPtr SetWinEventHook(SetWinEventHookType eventMin,
            SetWinEventHookType eventMax, IntPtr hmodWinEventProc,
            SetWinEventHookProc lpfnWinEventProc, uint idProcess,
            uint idThread, SetWinEventHookFlags dwFlags);

        /// <summary>
        /// Removes an event hook function created by a previous call to
        /// SetWinEventHook.
        /// </summary>
        /// <param name="hWinEventHook">
        /// Handle to the event hook returned in the previous call to
        /// SetWinEventHook.
        /// </param>
        /// <returns>
        /// If successful, returns TRUE; otherwise, returns FALSE.
        /// </returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        /// <summary>
        /// Retrieves the handle to the ancestor of the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose ancestor is to be retrieved.
        /// If this parameter is the desktop window, the function returns NULL. </param>
        /// <param name="flags">The ancestor to be retrieved.</param>
        /// <returns>The return value is the handle to the ancestor window.</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            out uint processId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd,
            StringBuilder lpString, int nMaxCount);
    }
}
