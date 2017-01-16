namespace timetracker.Messages.v3_15_5
{
	static class Constants
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

		internal const byte MessageHeader_Begin = (byte)'B';

		internal const byte MessageHeader_End = (byte)'E';
		internal const byte MessageHeader_KeppAlive = (byte)'K';

		// adding new definition and removing definition also emitted D tag
		internal const byte MessageHeader_ResolutionChange = (byte)'D';
		internal const byte MessageHeader_NetworkAdapter = (byte)'A';

		internal const byte MessageHeader_Memory = (byte)'I';
		internal const byte MessageHeader_NetworkBandwidth = (byte)'T';

		internal const byte MessageHeader_Version = (byte)'V';
	}
}
