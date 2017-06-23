using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using timetracker.BasePlugin.Messages;

namespace timetracker
{
	public partial class TrackSystem
	{
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
				: base(CurrentMessages.MessageHeader_MouseClick, x, y)
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
				: base(CurrentMessages.MessageHeader_MouseWheel, x, y)
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
				: base(CurrentMessages.MessageHeader_MouseMove, x, y)
			{
			}
		}

		public ulong MouseDistance
		{
			get;
			private set;
		}

		struct MouseCoords
		{
			public DateTime Time;
			public int X, Y;

			public MouseCoords(int x, int y, DateTime dt)
			{
				Time = dt;
				X = x;
				Y = y;
			}
		}

		RingBuffer<MouseCoords> MouseDistanceSpeedData = new RingBuffer<MouseCoords>(8);
		object MouseDistanceSpeedLocker = new object();

		public double MouseDistanceSpeed
		{
			get
			{
				if (MouseDistanceSpeedData.Count < 2)
					return 0;

				MouseCoords item;
				MouseDistanceSpeedData.Bottom(out item);

				DateTime low = item.Time;
				DateTime high = DateTime.Now;

				// remove old data
				lock (MouseDistanceSpeedLocker)
					MouseDistanceSpeedData.RemoveIf(m => m.Time.AddSeconds(5) < high);

				if (MouseDistanceSpeedData.Count < 2)
					return 0;

				ulong distance = 0;

				lock (MouseDistanceSpeedLocker)
				{
					foreach (var it in MouseDistanceSpeedData)
					{
						low = new DateTime(Math.Min(low.Ticks, it.Time.Ticks));

						distance += Distance(item.X, item.Y, it.X, it.Y);
						item = it;
					}
				}

				return distance / (high - low).TotalSeconds;
			}
		}

		public ulong MouseClickCount
		{
			get;
			private set;
		}

		RingBuffer<DateTime> MouseClickSpeedData = new RingBuffer<DateTime>(64);
		int LastX = 0;
		int LastY = 0;
		object MouseClickSpeedLocker = new object();

		public double MouseClickSpeed
		{
			get
			{
				DateTime min, max = DateTime.Now;
				TimeSpan span;
				int count;
				double time;

				if (!MouseClickSpeedData.Bottom(out min))
					return 0;

				// remove old data
				lock (MouseClickSpeedLocker)
					MouseClickSpeedData.RemoveIf(m => m.AddMinutes(5) < max);

				if (MouseClickSpeedData.Count < 2)
					return 0;

				span = max - min;

				if (span == TimeSpan.Zero)
					return 0;

				count = MouseClickSpeedData.Count;
				time = span.TotalMinutes;

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

				lock (MouseClickSpeedLocker)
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

			MouseDistance += Distance(LastX, LastY, x, y);

			lock (MouseDistanceSpeedLocker)
				MouseDistanceSpeedData.Add(new MouseCoords(x, y, dt));

			AppendBinary(new MouseMoveEventType(x, y), dt);

			LastMouseAction = dt;
			LastX = x;
			LastY = y;
		}

		private void MouseWheelEvent(MouseHook.MouseAxis axis, int value,
			int x, int y)
		{
			DateTime dt = DateTime.Now;

			AppendBinary(new MouseWheelEventType(x, y, axis, value), dt);

			LastMouseAction = dt;
		}

		private ulong Distance(int x1, int y1, int x2, int y2)
		{
			int xx = x2 - x1;
			int xy = y2 - y1;
			return (ulong)Math.Sqrt(xx * xx + xy * xy);
		}
	}
}
