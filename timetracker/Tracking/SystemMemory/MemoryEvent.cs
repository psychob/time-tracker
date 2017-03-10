using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;

namespace timetracker.Tracking.SystemMemory
{
	class MemoryEvent : IMessage
	{
		private ulong FreePhysicial, TotalPhysicial;
		private ulong FreeVirtual, TotalVirtual;
		private ulong AllFree, AllTotal;

		public MemoryEvent(ulong phy, ulong virt, ulong totalphy, ulong totalvirt)
		{
			FreePhysicial = phy;
			TotalPhysicial = totalphy;

			FreeVirtual = virt;
			TotalVirtual = totalvirt;

			AllFree = phy + virt;
			AllTotal = totalphy + totalvirt;
		}

		public string GetMessageType()
		{
			return Current.Messages.MemoryInfo;
		}

		public int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			throw new NotImplementedException();
		}
	}
}
