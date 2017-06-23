namespace timetracker.BasePlugin.Messages.v3_15_4
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

		// It was wrongly reported by class as B
		public const byte MessageHeader_End = (byte)'E';
		public const byte MessageHeader_KeppAlive = (byte)'K';

		public const byte MessageHeader_ResolutionChange = (byte)'D';
		public const byte MessageHeader_NetworkAdapter = (byte)'A';

		public const byte MessageHeader_Memory = (byte)'I';

		// should not be reported as E
		public const byte MessageHeader_NetworkBandwidth = (byte)'E';
	}
}
