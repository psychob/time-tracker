using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;

namespace timetracker.Tracking.ProcessorLoad
{
	class ProcessorLoadEvent : IMessage
	{
		private string Name;
		private int Work, Kernel, Idle;

		public ProcessorLoadEvent(string name, int work, int kernel, int idle)
		{
			Name = name;
			Work = work;
			Kernel = kernel;
			Idle = idle;
		}

		public string GetMessageType()
		{
			return Current.Messages.ProcessorLoad;
		}

		public int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			throw new NotImplementedException();
		}
	}
}
