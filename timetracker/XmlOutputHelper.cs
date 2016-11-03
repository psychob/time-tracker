using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WinAPI.WMI;
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

		private static void Node_Prologue(this XmlWriter xml, string node,
			DateTime dt)
		{
			xml.WriteStartElement(node);

			xml.WriteAttributeString("PreciseTime", dt.Ticks.ToString());

#if DEBUG
			xml.WriteAttributeString("DebugTime", dt.ToSensibleFormat());
#endif
		}

		private static void Node_Epilogue(this XmlWriter xml)
		{
		}

		private static void Node_Close(this XmlWriter xml)
		{
			xml.WriteEndElement();
		}

		private static void Node_DebugFlush(this XmlWriter xml)
		{
#if DEBUG
			xml.Node_Flush();
#endif
		}

		private static void Node_Flush(this XmlWriter xml)
		{
			xml.Flush();
		}

		public static void Node_MemoryInfo(this XmlWriter xml, DateTime dt,
			ulong PhysicalFree, ulong PhysicalTotal, string PhysicalRound,
			ulong VirtualFree, ulong VirtualTotal, string VirtualRound,
			ulong AllFree, ulong AllTotal, string AllRound)
		{
			xml.Node_Prologue(NodeName_MemoryInfo, dt);

			xml.WriteAttributeString("PhysicalFree", PhysicalFree.ToString());
			xml.WriteAttributeString("PhysicalTotal", PhysicalTotal.ToString());
			xml.WriteAttributeString("PhysicalProc", PhysicalRound);

			xml.WriteAttributeString("VirtualFree", VirtualFree.ToString());
			xml.WriteAttributeString("VirtualTotal", VirtualTotal.ToString());
			xml.WriteAttributeString("VirtualProc", VirtualRound);

			xml.WriteAttributeString("AllFree", AllFree.ToString());
			xml.WriteAttributeString("AllTotal", AllTotal.ToString());
			xml.WriteAttributeString("AllProc", AllRound);

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_DebugFlush();
		}

		public static void Node_NetworkAdapterEvent(this XmlWriter xml, DateTime dt,
			string name, Guid guid, Win32_NetworkAdapter.NetConnectionStatus status)
		{
			xml.Node_Prologue(NodeName_NetworkAdapterEvent, dt);

			xml.WriteAttributeString("GUID", guid.ToString());
			xml.WriteAttributeString("Name", name);
			xml.WriteAttributeString("Status", status.ToString());

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_DebugFlush();
		}
	}
}
