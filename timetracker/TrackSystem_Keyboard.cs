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
		Thread ThreadKeyboard;
		FileStream FileKeyboard = null;
		const int KEYBOARD_FIELD_SIZE = 1 + 8 + 4 + 4;
		BlockingCollection<KeyboardDataLine> QueueKeyboard = new BlockingCollection<KeyboardDataLine>(10 * 1024);
		byte[] BufferKeyboard = new byte[KEYBOARD_FIELD_SIZE * 1024];

		const byte KeyboardPress = (byte)'P';
		const byte KeyboardUnpress = (byte)'U';

		public long KeyboardStrokes
		{
			get;
			private set;
		}

		struct KeyboardDataLine
		{
			public byte Type;
			public long Ticks;
			public uint VK;
			public uint SC;

			public KeyboardDataLine(byte T, DateTime dt, uint vk, uint sc)
			{
				Type = T;
				Ticks = dt.Ticks;
				VK = vk;
				SC = sc;
			}
		}

		private void KeyboardThreadLoop()
		{
			int LastLength = 0;

			while (true)
			{
				Debug.WriteLine("Keyboard Data: {0}", QueueKeyboard.Count);

				if (LastLength > 0)
					Array.Clear(BufferKeyboard, 0, KEYBOARD_FIELD_SIZE * LastLength);

				int Processed = 0;

				KeyboardDataLine item;
				for (var it = 0; it < 1024 || StopTracking; ++it)
				{
					if (QueueKeyboard.TryTake(out item))
					{
						if (item.Type == KeyboardPress)
							KeyboardStrokes++;

						byte[] arr;

						// id
						BufferKeyboard[(Processed * KEYBOARD_FIELD_SIZE) + 0] = item.Type;

						// time
						arr = BitConverter.GetBytes(item.Ticks);
						arr.CopyTo(BufferKeyboard, (Processed * KEYBOARD_FIELD_SIZE) + 1);

						// X
						arr = BitConverter.GetBytes(item.VK);
						arr.CopyTo(BufferKeyboard, (Processed * MOUSE_FIELD_SIZE) + 8 + 1);

						// Y
						arr = BitConverter.GetBytes(item.SC);
						arr.CopyTo(BufferKeyboard, (Processed * MOUSE_FIELD_SIZE) + 8 + 1 + 4);

						Processed++;

						if (StopTracking && Processed == 1024)
						{
							FileKeyboard.Write(BufferKeyboard, 0, Processed * KEYBOARD_FIELD_SIZE);
							Array.Clear(BufferKeyboard, 0, KEYBOARD_FIELD_SIZE * Processed);

							Processed = 0;
						}
					} else
						break;
				}

				if (Processed > 0)
					FileKeyboard.Write(BufferKeyboard, 0, Processed * KEYBOARD_FIELD_SIZE);

				LastLength = Processed;

				if (StopTracking)
					break;

				Thread.Sleep(4500);
			}
		}

		private void KeyEvent(uint virtualKode, uint scanKode, bool up)
		{
			DateTime dt = DateTime.Now;

			KeyEventAppend(new KeyboardDataLine(up ? KeyboardPress : KeyboardUnpress,
				dt, virtualKode, scanKode));
		}

		private void KeyEventAppend(KeyboardDataLine keyboardDataLine)
		{
			QueueKeyboard.Add(keyboardDataLine);
		}
	}
}
