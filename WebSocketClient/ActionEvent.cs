﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPWebSocketClient
{
    public class ActionEvent : ActionCommand
    {
        public string action;
        public ActionEvent(string str)
        {
            action = str;
        }
        public void Action(ActionCommand e)
        {
            throw new NotImplementedException();
        }
    }
}
