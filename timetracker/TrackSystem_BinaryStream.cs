using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace timetracker
{
	public partial class TrackSystem
	{
		// interface
		internal interface TokenValue
		{
			int AsByteStream(ref byte[] str, int start, int length);
		}

		internal struct Token
		{
			public DateTime Time;
			public TokenValue Value;

			public Token(DateTime t, TokenValue v)
			{
				Time = t;
				Value = v;
			}
		}

		const int BinaryQueueSize = 1024 * 10;

		const byte BinaryStartMessage = (byte)'[';
		const byte BinaryStartInnerMessage = (byte)'{';
		const byte BinaryEndInnerMessage = (byte)'}';
		const byte BinaryEndMessage = (byte)']';

		const byte MessageHeader_MouseClick = (byte)'C';
		const byte MessageHeader_MouseMove = (byte)'M';
		const byte MessageHeader_MouseWheel = (byte)'W';
		const byte MessageHeader_KeyPressed = (byte)'P';
		const byte MessageHeader_KeyUnpressed = (byte)'U';
		const byte MessageHeader_ForegroundChange = (byte)'F';
		const byte MessageHeader_Processor = (byte)'R';
		const byte MessageHeader_Namechange = (byte)'N';

		const byte MessageHeader_Begin = (byte)'B';
		const byte MessageHeader_End = (byte)'E';
		const byte MessageHeader_KeppAlive = (byte)'K';

		const byte MessageHeader_ResolutionChange = (byte)'D';
		const byte MessageHeader_NetworkAdapter = (byte)'A';

		FileStream StreamBinary;
		Thread ThreadBinary;
		BlockingCollection<Token> QueueBinary = new BlockingCollection<Token>(BinaryQueueSize);
		bool BinaryStop = false;

		byte[] BinaryBuffer = new byte[1024 * 1024];

		void ThreadBinaryLoop()
		{
			while (true)
			{
				if (BinaryStop || QueueBinary.Count >= 1024)
				{
					Debug.WriteLine("Binary Queue: {0}", QueueBinary.Count);

					// we are saving everything
					int Bytes = 0;

					Token item;
					while (QueueBinary.TryTake(out item))
					{
						byte[] bytes;

						// first thing we output is start tag of token message
						BinaryBuffer[Bytes++] = BinaryStartMessage;

						// then we output ticks
						bytes = BitConverter.GetBytes(item.Time.Ticks);
						bytes.CopyTo(BinaryBuffer, Bytes);
						Bytes += bytes.Length;

						// next we indicate start of inner message
						BinaryBuffer[Bytes] = BinaryStartInnerMessage;
						Bytes++;

						// then we output message
						Bytes += item.Value.AsByteStream(ref BinaryBuffer, Bytes, BinaryBuffer.Length);

						// in the end we output End of inner message and end
						// of message
						BinaryBuffer[Bytes++] = BinaryEndInnerMessage;
						BinaryBuffer[Bytes++] = BinaryEndMessage;
					}

					// now we simply save data to file
					StreamBinary.Write(BinaryBuffer, 0, Bytes);
					StreamBinary.Flush();

					// and clear the buffer
					Array.Clear(BinaryBuffer, 0, Bytes);

					Debug.WriteLine("Saved bytes: {0}", Bytes);

					if (BinaryStop)
						break;
				} else
				{
					Debug.WriteLine("Binary Queue: {0}", QueueBinary.Count);
				}

				Thread.Sleep(1000);
			}
		}

		void AppendBinary(TokenValue value, DateTime? dt = null)
		{
			if (value == null)
				throw new NullReferenceException();

			DateTime tokenTime;

			if (dt.HasValue)
				tokenTime = dt.Value;
			else
				tokenTime = DateTime.Now;

			QueueBinary.Add(new Token(tokenTime, value));
		}
	}
}
