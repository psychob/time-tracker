using System;
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
		Thread ThreadMouseProcess;
		FileStream FileMouseData = null;
		const int MOUSE_FIELD_SIZE = 25;
		BlockingCollection<MouseDataLine> QueueMouse = new BlockingCollection<MouseDataLine>(10 * 1024);
		byte[] BufferMouseData = new byte[MOUSE_FIELD_SIZE * 1024];

		public long MouseDistance
		{
			get;
			private set;
		}

		const byte MouseClick = (byte)'C';
		const byte MouseMove = (byte)'M';
		const byte MouseWheel = (byte)'W';

		MouseDataLine LastMDL = new MouseDataLine();

		public struct MouseDataLine
		{
			public byte Type;
			public long Ticks;
			public int X;
			public int Y;
			public MouseHook.MouseButton Btn;
			public MouseHook.MouseAxis Axis;
			public bool Pressed;
			public int Value;

			public MouseDataLine(DateTime dt, int x, int y)
			{
				Type = MouseMove;
				Ticks = dt.Ticks;
				X = x;
				Y = y;
				Btn = MouseHook.MouseButton.Left;
				Axis = MouseHook.MouseAxis.Horizontal;
				Pressed = false;
				Value = 0;
			}

			public MouseDataLine(DateTime dt, int x, int y,
				MouseHook.MouseButton btn, bool pressed)
			{
				Type = MouseClick;
				Ticks = dt.Ticks;
				X = x;
				Y = y;
				Btn = btn;
				Axis = MouseHook.MouseAxis.Horizontal;
				Pressed = pressed;
				Value = 0;
			}

			public MouseDataLine(DateTime dt, int x, int y,
				MouseHook.MouseAxis axe, int value)
			{
				Type = MouseClick;
				Ticks = dt.Ticks;
				X = x;
				Y = y;
				Btn = MouseHook.MouseButton.Left;
				Axis = axe;
				Pressed = false;
				Value = value;
			}
		}

		private void MouseThreadLoop()
		{
			int LastLength = 0;

			while (true)
			{
				Debug.WriteLine("Mouse Data: {0}", QueueMouse.Count);

				if (LastLength > 0)
					Array.Clear(BufferMouseData, 0, MOUSE_FIELD_SIZE * LastLength);

				int Processed = 0;

				MouseDataLine item;
				for (var it = 0; it < 1024 || StopTracking; ++it)
				{
					if (QueueMouse.TryTake(out item))
					{
						MouseDistance += (int)Math.Sqrt((LastMDL.X - item.X) * (LastMDL.X - item.X) + (LastMDL.Y - item.Y) * (LastMDL.Y - item.Y));
						LastMDL = item;

						byte[] arr;

						// id
						BufferMouseData[(Processed * MOUSE_FIELD_SIZE) + 0] = item.Type;

						// time
						arr = BitConverter.GetBytes(item.Ticks);
						arr.CopyTo(BufferMouseData, (Processed * MOUSE_FIELD_SIZE) + 1);

						// X
						arr = BitConverter.GetBytes(item.X);
						arr.CopyTo(BufferMouseData, (Processed * MOUSE_FIELD_SIZE) + 8 + 1);

						// Y
						arr = BitConverter.GetBytes(item.Y);
						arr.CopyTo(BufferMouseData, (Processed * MOUSE_FIELD_SIZE) + 8 + 1 + 4);

						switch (item.Type)
						{
							case MouseClick:
								// button
								arr = BitConverter.GetBytes((int)item.Btn);
								arr.CopyTo(BufferMouseData, (Processed * MOUSE_FIELD_SIZE) + 8 + 1 + 4 + 4);

								// pressed
								arr = BitConverter.GetBytes(item.Pressed);
								arr.CopyTo(BufferMouseData, (Processed * MOUSE_FIELD_SIZE) + 8 + 1 + 4 + 4 + 4);
								break;

							case MouseWheel:
								// button
								arr = BitConverter.GetBytes((int)item.Axis);
								arr.CopyTo(BufferMouseData, (Processed * MOUSE_FIELD_SIZE) + 8 + 1 + 4 + 4);

								// pressed
								arr = BitConverter.GetBytes(item.Value);
								arr.CopyTo(BufferMouseData, (Processed * MOUSE_FIELD_SIZE) + 8 + 1 + 4 + 4 + 4);
								break;
						}

						Processed++;

						if (StopTracking && Processed == 1024)
						{
							FileMouseData.Write(BufferMouseData, 0, Processed * MOUSE_FIELD_SIZE);
							Array.Clear(BufferMouseData, 0, MOUSE_FIELD_SIZE * Processed);

							Processed = 0;
						}
					} else
						break;
				}

				if (Processed > 0)
				{
					FileMouseData.Write(BufferMouseData, 0, Processed * MOUSE_FIELD_SIZE);
				}

				LastLength = Processed;

				if (StopTracking)
					break;

				Thread.Sleep(4500);
			}
		}

		private void MouseAppendDataLog(MouseDataLine s)
		{
			QueueMouse.Add(s);
		}

		DateTime LastMouseAction;

		private void MouseClickEvent(MouseHook.MouseButton btn, bool pressed,
			int x, int y)
		{
			DateTime dt = DateTime.Now;

			MouseAppendDataLog(new MouseDataLine(dt, x, y, btn, pressed));

			LastMouseAction = dt;
		}

		private void MouseMoveEvent(int x, int y)
		{
			DateTime dt = DateTime.Now;

			if (LastMouseAction.AddMilliseconds(100) >= dt)
				return;

			MouseAppendDataLog(new MouseDataLine(dt, x, y));

			LastMouseAction = dt;
		}

		private void MouseWheelEvent(MouseHook.MouseAxis axis, int value,
			int x, int y)
		{
			DateTime dt = DateTime.Now;

			MouseAppendDataLog(new MouseDataLine(dt, x, y, axis, value));

			LastMouseAction = dt;
		}
	}
}
