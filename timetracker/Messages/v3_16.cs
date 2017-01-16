namespace timetracker.Messages.v3_16
{
	internal static class Constants
	{
		internal const byte BinaryStartMessage = (byte)'[';
		internal const byte BinaryStartInnerMessage = (byte)'{';
		internal const byte BinaryEndInnerMessage = (byte)'}';
		internal const byte BinaryEndMessage = (byte)']';

		internal const byte MessageHeader_MouseClick = (byte)'C';
		internal const byte MessageHeader_MouseMove = (byte)'M';
		internal const byte MessageHeader_MouseWheel = (byte)'W';
		internal const byte MessageHeader_KeyPressed = (byte)'P';
		internal const byte MessageHeader_KeyUnpressed = (byte)'U';
		internal const byte MessageHeader_ForegroundChange = (byte)'F';
		internal const byte MessageHeader_Processor = (byte)'R';
		internal const byte MessageHeader_Namechange = (byte)'N';

		// extended Begin to add ParentID
		internal const byte MessageHeader_Begin = (byte)'B';

		internal const byte MessageHeader_End = (byte)'E';
		internal const byte MessageHeader_KeppAlive = (byte)'K';

		internal const byte MessageHeader_ResolutionChange = (byte)'D';
		internal const byte MessageHeader_NetworkAdapter = (byte)'A';

		internal const byte MessageHeader_Memory = (byte)'I';
		internal const byte MessageHeader_NetworkBandwidth = (byte)'T';

		internal const byte MessageHeader_Version = (byte)'V';

		internal const byte MessageHeader_AddDefinition = (byte)'a';
		internal const byte MessageHeader_RemoveDefinition = (byte)'d';
	}
}
