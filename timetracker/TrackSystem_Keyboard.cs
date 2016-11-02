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
		const byte KeyboardPress = (byte)'P';
		const byte KeyboardUnpress = (byte)'U';

		class KeyboardTokenPress : TokenValue
		{
			public byte Type;
			public uint VirtualKey;
			public uint ScanCode;

			public KeyboardTokenPress(bool pressed, uint vk, uint sc)
			{
				Type = pressed ? KeyboardPress : KeyboardUnpress;
				VirtualKey = vk;
				ScanCode = sc;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				// vk
				buff = BitConverter.GetBytes(VirtualKey);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				// sc
				buff = BitConverter.GetBytes(ScanCode);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		internal long KeyboardStrokes
		{
			get;
			private set;
		}

		RingBuffer<DateTime> KeyboardSpeedData = new RingBuffer<DateTime>(512);

		internal double KeyboardSpeed
		{
			get
			{
				DateTime min, max;
				TimeSpan span;
				int count;
				double time;

				if (!KeyboardSpeedData.Bottom(out min))
					return 0;

				if (!KeyboardSpeedData.Top(out max))
					return 0;

				span = max - min;

				if (span == TimeSpan.Zero)
					return 0;

				count = KeyboardSpeedData.Count;
				time = span.TotalMinutes;

				Debug.WriteLine("{0} - {1}", span.TotalMinutes,
					KeyboardSpeedData.Count);

				return count / time;
			}
		}

		private void KeyEvent(uint virtualKode, uint scanKode, bool up)
		{
			DateTime dt = DateTime.Now;

			if (!up)
			{
				KeyboardStrokes++;
				KeyboardSpeedData.Add(dt);
			}

			AppendBinary(new KeyboardTokenPress(up, virtualKode, scanKode), dt);
		}
	}
}
