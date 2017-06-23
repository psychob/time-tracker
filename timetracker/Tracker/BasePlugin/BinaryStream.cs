using log4net;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using timetracker.BasePlugin;
using timetracker.BasePlugin.Messages;

namespace timetracker.Tracker.BasePlugin
{
	class BinaryStream : IBinaryStream
	{
		public readonly string FileHeader = "BINARY DATA FIL2";

		private string FilePath;
		private ILog Logger = null;
		private IConfigurationStorage Config = null;
		private Thread SavingThread = null;
		private int ThreadDelay = 0;
		private int ThreadAmountFlush = 0;
		private int BufferSize = 0;
		private int QueueSize = 0;
		private bool StopThread = false;
		private bool ForceFlush = false;
		private FileStream Output = null;
		private BlockingCollection<Token> Queue = null;

		public BinaryStream(string path, ILog logger, IConfigurationStorage config)
		{
			FilePath = path;
			Logger = logger;
			Config = config;

			SetUp();
		}

		private void SetUp()
		{
			Logger.Debug("Setting up BinaryStream");

			SavingThread = new Thread(ThreadLoop);
			SavingThread.Name = Config.GetValue("ThreadName", "BinaryStream.Save");
			SavingThread.Start();

			Logger.Debug("Thread Started");

			ThreadDelay = Config.GetInteger("ThreadDealy", 1000);
			ThreadAmountFlush = Config.GetInteger("ThreadAmountFlush", 1024);
			BufferSize = Config.GetInteger("BufferSize", 1024 * 1024);
			QueueSize = Config.GetInteger("QueueSize", 1024 * 10);

			Logger.DebugFormat("Retrived parameters: ThreadDealy={0} ThreadAmountFlush={1} BufferSize={2} QueueSize={3}",
				ThreadDelay, ThreadAmountFlush, BufferSize, QueueSize);

			Logger.DebugFormat("Opening file stream: {0}", FilePath);
			Output = new FileStream(FilePath, FileMode.Create,
				FileAccess.Write, FileShare.Read);

			Logger.Debug("Starting Queue");
			Queue = new BlockingCollection<Token>(QueueSize);
		}

		private void ThreadLoop()
		{
			byte[] WriteBuffer = new byte[BufferSize];
			Output.WriteBytes(FileHeader);

			while(true)
			{
				if (StopThread || ForceFlush || Queue.Count >= ThreadAmountFlush)
				{
					Logger.Debug("Flushing Queue to stream");
					Logger.DebugFormat("Element Count in Queue: {0}", Queue.Count);

					Token token;
					int byteOffset = 0;

					while (Queue.TryTake(out token))
					{
						ConstructMessage(token, ref WriteBuffer, ref byteOffset);
					}

					Output.Write(WriteBuffer, 0, byteOffset);
					Output.Flush();

					Logger.DebugFormat("Saved Bytes: {0}", byteOffset);
					ForceFlush = false;

					if (StopThread && Queue.Count == 0)
						break;
				} else
				{
					Logger.DebugFormat("Element Count in Queue: {0}", Queue.Count);
				}

				if (!StopThread)
					Thread.Sleep(ThreadDelay);
			}
		}

		private void ConstructMessage(Token item,
			ref byte[] BinaryBuffer, ref int Bytes)
		{
			byte[] bytes;

			// first thing we output is start tag of token message
			BinaryBuffer[Bytes++] = CurrentMessages.BinaryStartMessage;

			// then we output ticks
			bytes = BitConverter.GetBytes(item.Time.Ticks);
			bytes.CopyTo(BinaryBuffer, Bytes);
			Bytes += bytes.Length;

			// next we indicate start of inner message
			BinaryBuffer[Bytes] = CurrentMessages.BinaryStartInnerMessage;
			Bytes++;

			// then we output message
			bytes = item.Value.ToBinaryStream();
			bytes.CopyTo(BinaryBuffer, Bytes);
			Bytes += bytes.Length;

			// in the end we output End of inner message and end
			// of message
			BinaryBuffer[Bytes++] = CurrentMessages.BinaryEndInnerMessage;
			BinaryBuffer[Bytes++] = CurrentMessages.BinaryEndMessage;
		}

		public void Flush()
		{
			ForceFlush = true;
		}

		public void Send(IToken token, DateTime? dt = default(DateTime?), bool flush = false)
		{
			if (token == null)
				throw new NullReferenceException();

			Token stoken = new Token(dt.HasValue ? dt.Value : DateTime.Now, token);

			Queue.Add(stoken);

			if (flush)
				Flush();
		}
	}
}
