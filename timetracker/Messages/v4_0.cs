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

		public string MouseMove { get { return "MOV"; } }

		public string MousePressRight { get { return "MPR"; } }

		public string MouseReleaseRight { get { return "MRR"; } }

		public string MousePressLeft { get { return "MPL"; } }

		public string MouseReleaseLeft { get { return "MRL"; } }

		public string MousePressMiddle { get { return "MPM"; } }

		public string MouseReleaseMiddle { get { return "MRM"; } }

		public string MousePressXn { get { return "MPX"; } }

		public string MouseReleaseXn { get { return "MRX"; } }

		public string MouseWheelVertical { get { return "MWV"; } }

		public string MouseWheelHorizontal { get { return "MWH"; } }
	}
}