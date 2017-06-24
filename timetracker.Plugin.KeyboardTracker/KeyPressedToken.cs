﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.BasePlugin.Messages;

namespace timetracker.Plugin.KeyboardTracker
{
    public class KeyPressedToken : KeyboardToken
    {
        public KeyPressedToken(uint virtualKey, uint scanCode)
            : base(virtualKey, scanCode)
        {
        }

        public override string GetInnerType()
        {
            return CurrentMessages.MessageHeader_KeyPressed;
        }
    }
}
