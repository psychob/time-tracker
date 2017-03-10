using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using timetracker.WinAPI.WMI;
using static timetracker.TrackSystem;

namespace timetracker
{
	public static class XmlOutputHelper
	{
		// earlier
		const string NodeName_Begin = "begin";
		const string NodeName_End   = "end";
		const string NodeName_Ping  = "ping";

		// released in 3.0.9
		const string NodeName_WindowForegroundChanged = "win-fgc";
		const string NodeName_KeyPressed              = "key-ipr";
		const string NodeName_KeyUnpressed            = "key-upr";
		const string NodeName_MemoryInfo              = "s-mem-i";
		const string NodeName_NetworkAdapterEvent     = "net-chg";
		const string NodeName_NameChange              = "obj-nch";
		const string NodeName_ResolutionChange        = "res-chn";
		const string NodeName_NewDefinition           = "new-app";
		const string NodeName_RemoveDefinition        = "rem-app";
		const string NodeName_MouseMove               = "mouse-m";
		const string NodeName_MouseClickPressed       = "mouse-i";
		const string NodeName_MouseClickUnpressed     = "mouse-u";
		const string NodeName_MouseWheel              = "mouse-w";

		// released in 3.0.10
		const string NodeName_ProcessorLoad = "proc-lod";
	}
}
