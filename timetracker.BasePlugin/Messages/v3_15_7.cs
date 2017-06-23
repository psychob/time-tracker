namespace timetracker.BasePlugin.Messages.v3_15_7
{
	public class Constants
	{
		public const byte BinaryStartMessage = (byte)'[';
		public const byte BinaryStartInnerMessage = (byte)'{';
		public const byte BinaryEndInnerMessage = (byte)'}';
		public const byte BinaryEndMessage = (byte)']';

		public const byte MessageHeader_MouseClick = (byte)'C';
		public const byte MessageHeader_MouseMove = (byte)'M';
		public const byte MessageHeader_MouseWheel = (byte)'W';
		public const byte MessageHeader_KeyPressed = (byte)'P';
		public const byte MessageHeader_KeyUnpressed = (byte)'U';
		public const byte MessageHeader_ForegroundChange = (byte)'F';
		public const byte MessageHeader_Processor = (byte)'R';
		public const byte MessageHeader_Namechange = (byte)'N';

		public const byte MessageHeader_Begin = (byte)'B';

		public const byte MessageHeader_End = (byte)'E';
		public const byte MessageHeader_KeppAlive = (byte)'K';

		public const byte MessageHeader_ResolutionChange = (byte)'D';
		public const byte MessageHeader_NetworkAdapter = (byte)'A';

		public const byte MessageHeader_Memory = (byte)'I';
		public const byte MessageHeader_NetworkBandwidth = (byte)'T';

		public const byte MessageHeader_Version = (byte)'V';

		public const byte MessageHeader_AddDefinition = (byte)'a';
		public const byte MessageHeader_RemoveDefinition = (byte)'d';
	}
}
