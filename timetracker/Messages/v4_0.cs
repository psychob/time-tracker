﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Messages
{
	internal class v4_0
	{
		public string MemoryInfo { get { return "MEM"; } }

		public string ProcessorLoad { get { return "PRC"; } }

		public string NetworkLoad { get { return "NET"; } }

		public string KeyboardRelease { get { return "KRE"; } }

		public string KeyboardPress { get { return "KPR"; } }
	}
}
