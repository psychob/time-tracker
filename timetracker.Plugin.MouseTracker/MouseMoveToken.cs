using timetracker.BasePlugin;
using timetracker.BasePlugin.Messages;

namespace timetracker.Plugin.MouseTracker
{
    class MouseMoveToken : MouseToken
    {
        public MouseMoveToken(int x, int y)
            : base(CurrentMessages.MessageHeader_MouseMove, x, y)
        {
        }
    }
}