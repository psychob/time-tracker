﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

		public static void Node_WindowForegroundChanged(this XmlWriter xml,
			DateTime dt, uint ProcessID)
		{
			xml.Node_Prologue(NodeName_WindowForegroundChanged, dt);

			xml.WriteAttributeString("PID", ProcessID.ToString());

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_DebugFlush();
		}

		public static void Node_KeyPressed(this XmlWriter xml,
			DateTime dt, uint vk, uint sc, bool pressed)
		{
			if (pressed)
				xml.Node_Prologue(NodeName_KeyPressed, dt);
			else
				xml.Node_Prologue(NodeName_KeyUnpressed, dt);

			xml.WriteAttributeString("VirtualKey", vk.ToString());
			xml.WriteAttributeString("ScanKode", sc.ToString());

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_DebugFlush();
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
			string name, Guid guid, WinAPI.NetConnectionStatus status)
		{
			xml.Node_Prologue(NodeName_NetworkAdapterEvent, dt);

			xml.WriteAttributeString("GUID", guid.ToString());
			xml.WriteAttributeString("Name", name);
			xml.WriteAttributeString("Status", status.ToString());

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_DebugFlush();
		}

		public static void Node_NameChange(this XmlWriter xml, DateTime dt,
			uint pid, string title)
		{
			xml.Node_Prologue(NodeName_NameChange, dt);

			xml.WriteAttributeString("PID", pid.ToString());

			xml.Node_Epilogue();

			xml.WriteValue(title);
			xml.Node_Close();

			xml.Node_DebugFlush();
		}

		public static void Node_ResolutionChanged(this XmlWriter xml, DateTime dt,
			int width, int heigth)
		{
			xml.Node_Prologue(NodeName_ResolutionChange, dt);

			xml.WriteAttributeString("Width", width.ToString());
			xml.WriteAttributeString("Height", heigth.ToString());

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_DebugFlush();
		}

		public static void Node_NewDefinition(this XmlWriter xml, DateTime dt,
			string uniqueId, string appName)
		{
			xml.Node_Prologue(NodeName_NewDefinition, dt);

			xml.WriteAttributeString("UniqueID", uniqueId);

			xml.Node_Epilogue();

			xml.WriteValue(appName);
			xml.Node_Close();

			xml.Node_Flush();
		}

		public static void Node_RemoveDefinition(this XmlWriter xml, DateTime dt,
			string uniqueId)
		{
			xml.Node_Prologue(NodeName_RemoveDefinition, dt);

			xml.WriteAttributeString("UniqueID", uniqueId);

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_Flush();
		}

		public static void Node_Begin(this XmlWriter xml, DateTime dt,
			int PID, string appTag, string ruleSetID)
		{
			xml.Node_Prologue(NodeName_Begin, dt);

			xml.WriteAttributeString("PID", PID.ToString());
			xml.WriteAttributeString("App", appTag);
			xml.WriteAttributeString("RuleSet", ruleSetID);

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_DebugFlush();
		}

		public static void Node_End(this XmlWriter xml, DateTime dt,
			int PID, string appTag, ulong elapsed)
		{
			xml.Node_Prologue(NodeName_End, dt);

			xml.WriteAttributeString("PID", PID.ToString());
			xml.WriteAttributeString("App", appTag);
			xml.WriteAttributeString("Elapsed", elapsed.ToString());

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_DebugFlush();
		}


		public static void Node_Ping(this XmlWriter xml, DateTime dt)
		{
			xml.Node_Prologue(NodeName_Ping, dt);

			xml.Node_Epilogue();
			xml.Node_Close();

			xml.Node_DebugFlush();
		}
	}
}
