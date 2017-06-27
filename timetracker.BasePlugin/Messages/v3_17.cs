using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.BasePlugin.Messages.v3_17
{
    public class Constants
    {
        public const byte BinaryStartMessage = (byte)'[';
        public const byte BinaryStartInnerMessage = (byte)'{';
        public const byte BinaryEndInnerMessage = (byte)'}';
        public const byte BinaryEndMessage = (byte)']';

        public const string MessageHeader_MousePressClick = "$MP";
        public const string MessageHeader_MouseUnpressClick = "$MU";
        public const string MessageHeader_MouseVerticalWheel = "$MV";
        public const string MessageHeader_MouseHorizontalWheel = "$MH";
        public const string MessageHeader_MouseMove = "$MM";

        public const string MessageHeader_KeyPressed = "$KP";
        public const string MessageHeader_KeyUnpressed = "$KU";

        public const string MessageHeader_ForegroundChange = "FOR";

        public const byte MessageHeader_Processor = (byte)'R';
        public const byte MessageHeader_Namechange = (byte)'N';

        public const byte MessageHeader_Begin = (byte)'B';

        public const byte MessageHeader_End = (byte)'E';
        public const byte MessageHeader_KeppAlive = (byte)'K';

        public const byte MessageHeader_ResolutionChange = (byte)'D';
        public const byte MessageHeader_NetworkAdapter = (byte)'A';

        public const string MessageHeader_Memory = "MEM";
        public const byte MessageHeader_NetworkBandwidth = (byte)'T';

        public const byte MessageHeader_Version = (byte)'V';

        public const byte MessageHeader_AddDefinition = (byte)'a';
        public const byte MessageHeader_RemoveDefinition = (byte)'d';
    }
}
