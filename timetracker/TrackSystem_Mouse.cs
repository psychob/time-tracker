﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace timetracker
{
	public partial class TrackSystem
	{
		const byte MessageHeader_MouseClick = (byte)'C';
		const byte MessageHeader_MouseMove = (byte)'M';
		const byte MessageHeader_MouseWheel = (byte)'W';

		abstract class MouseBaseEvent : TokenValue
		{
			public byte Type;
			public int X;
			public int Y;

			public MouseBaseEvent(byte t, int x, int y)
			{
				Type = t;
				X = x;
				Y = y;
			}

			public virtual int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				// vk
				buff = BitConverter.GetBytes(X);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				// sc
				buff = BitConverter.GetBytes(Y);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		class MouseClickEventType : MouseBaseEvent
		{
			public MouseHook.MouseButton Button;
			public bool Pressed;

			public MouseClickEventType(int x, int y, MouseHook.MouseButton btn,
				bool press)
				: base(MessageHeader_MouseClick, x, y)
			{
				Button = btn;
				Pressed = press;
			}

			public override int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = base.AsByteStream(ref str, start, length);
				byte[] buff;

				// vk
				buff = BitConverter.GetBytes((int)Button);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				// sc
				buff = BitConverter.GetBytes(Pressed);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		class MouseWheelEventType : MouseBaseEvent
		{
			public MouseHook.MouseAxis Axis;
			public int Value;

			public MouseWheelEventType(int x, int y, MouseHook.MouseAxis a,
				int value)
				: base(MessageHeader_MouseWheel, x, y)
			{
				Axis = a;
				Value = value;
			}

			public override int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = base.AsByteStream(ref str, start, length);
				byte[] buff;

				// vk
				buff = BitConverter.GetBytes((int)Axis);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				// sc
				buff = BitConverter.GetBytes(Value);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		class MouseMoveEventType : MouseBaseEvent
		{
			public MouseMoveEventType(int x, int y)
				: base(MessageHeader_MouseMove, x, y)
			{
			}
		}

		public long MouseDistance
		{
			get;
			private set;
		}

		public long MouseClickCount
		{
			get;
			private set;
		}

		RingBuffer<DateTime> MouseClickSpeedData = new RingBuffer<DateTime>(128);

		public double MouseClickSpeed
		{
			get
			{
				DateTime min, max;
				TimeSpan span;
				int count;
				double time;

				if (!MouseClickSpeedData.Bottom(out min))
					return 0;

				if (!MouseClickSpeedData.Top(out max))
					return 0;

				span = max - min;

				if (span == TimeSpan.Zero)
					return 0;

				count = MouseClickSpeedData.Count;
				time = span.TotalMinutes;

				Debug.WriteLine("{0} - {1}", span.TotalMinutes,
					MouseClickSpeedData.Count);

				return count / time;
			}
		}

		DateTime LastMouseAction;

		private void MouseClickEvent(MouseHook.MouseButton btn, bool pressed,
			int x, int y)
		{
			DateTime dt = DateTime.Now;

			if (!pressed)
			{
				MouseClickCount++;
				MouseClickSpeedData.Add(dt);
			}

			AppendBinary(new MouseClickEventType(x, y, btn, pressed), dt);

			LastMouseAction = dt;
		}

		private void MouseMoveEvent(int x, int y)
		{
			DateTime dt = DateTime.Now;

			if (LastMouseAction.AddMilliseconds(100) >= dt)
				return;

			AppendBinary(new MouseMoveEventType(x, y), dt);

			LastMouseAction = dt;
		}

		private void MouseWheelEvent(MouseHook.MouseAxis axis, int value,
			int x, int y)
		{
			DateTime dt = DateTime.Now;

			AppendBinary(new MouseWheelEventType(x, y, axis, value), dt);

			LastMouseAction = dt;
		}
	}
}
